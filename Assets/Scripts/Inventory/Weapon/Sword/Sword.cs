using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject swingPrefab;
    [SerializeField] private Transform swingTransform;
    [SerializeField] private GameObject swordCollider;
    [SerializeField] private WeponInfoSO weaponInfo;

    private Animator animator;
    private GameObject swingAnim;

    private const string ATTACK_TRIGER = "Attack";


    private void Awake()
    {
        animator = GetComponent<Animator>();

        DamageDeal damage = swordCollider.GetComponent<DamageDeal>();
        damage.SetDamage(weaponInfo.damage);
    }

    private void Update()
    {
        MouseFollow();
    }

    public void Attack()
    {
        animator.SetTrigger(ATTACK_TRIGER);

        swingAnim = Instantiate(swingPrefab, swingTransform.position, Quaternion.identity);

        swingAnim.transform.parent = this.transform.parent;
        swordCollider.SetActive(true);
    }

    private void SwingFlipUpAnimation()
    {
        swingAnim.transform.Rotate(180, 0, 0);

        if (PlayerControl.Instanse.FlipedSide)
        {
            swingAnim.transform.Rotate(0, 180, 0);
        }
    }

    private void SwingFlipDownAnimation()
    {
        swingAnim.transform.Rotate(0, 0, 0);

        if (PlayerControl.Instanse.FlipedSide)
        {
            swingAnim.transform.Rotate(0, 180, 0);
        }
    }

    private void EndSwingAnimationEvent()
    {
        swordCollider.SetActive(false);
    }

    public void MouseFollow()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angleZ = Mathf.Atan2(mousePositionInWorld.y - PlayerControl.Instanse.transform.position.y, Mathf.Abs(mousePositionInWorld.x - PlayerControl.Instanse.transform.position.x)) * Mathf.Rad2Deg;

        bool flipX = mousePositionInWorld.x < PlayerControl.Instanse.transform.position.x;

        ActiveWeapon.Instanse.transform.rotation = Quaternion.Euler(0f, flipX ? -180f : 0, angleZ);
    }

    public WeponInfoSO GetWeaponInfoSo()
    {
        return weaponInfo;
    }
}
