using UnityEngine;

public class ActiveInventory : Singlton<ActiveInventory>
{
    private int activeSlot = 0;

    private PlayerControls playerControl;

    protected override void Awake()
    {
        base.Awake();

        playerControl = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        if (playerControl != null)
        {
            playerControl.Disable();
        }
    }

    private void Start()
    {
        playerControl.Inventory.ChangeSlot.performed += ctx => TogleActiveSlot((int)ctx.ReadValue<float>());
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
        ActivePotion.Instanse.ChangeActivePotion(hasPotion ? SpawnObject(potionInfoSO.potionPrefab, ActivePotion.Instanse.transform, ActivePotion.Instanse.transform.rotation) : null);
    }

    private MonoBehaviour SpawnObject(GameObject prefab, Transform itemTransform, Quaternion itemRotation)
    {
        GameObject newItem = Instantiate(prefab, itemTransform.position, itemRotation);
        newItem.transform.parent = itemTransform;
        return newItem.GetComponent<MonoBehaviour>();
    }

}
