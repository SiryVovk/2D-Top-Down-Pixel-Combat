using TMPro;
using UnityEngine;

public class ActiveInventory : Singlton<ActiveInventory>
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text staminaText;

    private int activeSlot = 0;
    private int startingNumberOfHealthPotions = 3;
    private int staringNumberOfStaminaPotions = 3;
    private int numberOfHealthPotions;
    private int numberOfStaminaPotions;

    private PlayerControls playerControl;

    public int NumberOfHealthPotions { get { return numberOfHealthPotions; } private set { numberOfHealthPotions = value; } }
    public int NumberOfStaminaPotions { get { return numberOfStaminaPotions; } private set { numberOfStaminaPotions = value; } }

    protected override void Awake()
    {
        base.Awake();

        playerControl = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControl.Enable();
        PlayerHelth.OnPlayerDeath += RefreshInventoryOnDeath;
    }

    private void OnDisable()
    {
        if (playerControl != null)
        {
            playerControl.Disable();
        }

        PlayerHelth.OnPlayerDeath -= RefreshInventoryOnDeath;
    }

    private void Start()
    {
        playerControl.Inventory.ChangeSlot.performed += ctx => TogleActiveSlot((int)ctx.ReadValue<float>());

        numberOfHealthPotions = startingNumberOfHealthPotions;
        numberOfStaminaPotions = staringNumberOfStaminaPotions;
        UpdetePotionNumbersText();
    }

    public void EqipFirstWeapon()
    {
        ToggleActiveHighliht(0);
    }

    private void TogleActiveSlot(int numVal)
    {
        ToggleActiveHighliht(numVal - 1);
    }

    private void ToggleActiveHighliht(int indexNum)
    {
        activeSlot = indexNum;

        foreach (Transform slot in transform)
        {
            slot.GetChild(0).gameObject.SetActive(false);
        }


        transform.GetChild(activeSlot).transform.GetChild(0).gameObject.SetActive(true);
        ChangeActiveItem();
    }

    private void ChangeActiveItem()
    {
        if (PlayerHelth.Instanse.IsDead)
        {
            return;
        }

        DestroyCurrentItem();

        InventarySlot inventarySlot = transform.GetChild(activeSlot).GetComponent<InventarySlot>();

        WeponInfoSO weponInfoSO = inventarySlot.GetWeaponInfo();
        PotionInfoSO potionInfoSO = inventarySlot.GetPotionInfo();

        UpdateActiveItem(weponInfoSO, potionInfoSO);
    }

    private void DestroyCurrentItem()
    {
        if (ActiveWeapon.Instanse.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instanse.CurrentActiveWeapon.gameObject);
        }

        if (ActivePotion.Instanse.CurrentActivePotion != null)
        {
            Destroy(ActivePotion.Instanse.CurrentActivePotion.gameObject);
        }
    }

    private void UpdateActiveItem(WeponInfoSO weponInfoSO, PotionInfoSO potionInfoSO)
    {
        bool hasWeapon = weponInfoSO != null;
        bool hasPotion = potionInfoSO != null;

        ActiveWeapon.Instanse.ChangeWeapon(hasWeapon ? SpawnObject(weponInfoSO.weaponPrefab, ActiveWeapon.Instanse.transform, ActiveWeapon.Instanse.transform.rotation) : null);
        ActivePotion.Instanse.ChangeActivePotion(hasPotion ? SpawnObject(potionInfoSO.potionPrefab, ActivePotion.Instanse.transform, Quaternion.identity) : null);
    }

    private MonoBehaviour SpawnObject(GameObject prefab, Transform itemTransform, Quaternion itemRotation)
    {
        GameObject newItem = Instantiate(prefab, itemTransform.position, itemRotation);
        newItem.transform.parent = itemTransform;
        return newItem.GetComponent<MonoBehaviour>();
    }

    private void UpdetePotionNumbersText()
    {
        healthText.text = numberOfHealthPotions.ToString();
        staminaText.text = numberOfStaminaPotions.ToString();
    }

    public void DecreceNumberOfHealthPotions()
    {
        numberOfHealthPotions--;
        UpdetePotionNumbersText();
    }

    public void DecreceNumberOfStaminaPotions()
    {
        numberOfStaminaPotions--;
        UpdetePotionNumbersText();
    }

    private void RefreshInventoryOnDeath()
    {
        numberOfHealthPotions = startingNumberOfHealthPotions;
        numberOfStaminaPotions = staringNumberOfStaminaPotions;
        UpdetePotionNumbersText();
    }
}
