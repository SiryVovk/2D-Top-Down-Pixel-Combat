using UnityEngine;

public class ActivePotion : Singlton<ActivePotion>
{
    private MonoBehaviour currentActivePotion;
    private PlayerControls playerControls;

    public MonoBehaviour CurrentActivePotion { get { return currentActivePotion; } private set { currentActivePotion = value; } }

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Disable();
        }
    }

    private void Start()
    {
        playerControls.Combat.UsePotion.started += _ => UsePotion();
    }

    private void UsePotion()
    {
        (currentActivePotion as IPotion).UsePotion();
    }

    public void ChangeActivePotion(MonoBehaviour potion)
    {
        CurrentActivePotion = potion;

        if (!potion)
        {
            return;
        }
    }
}
