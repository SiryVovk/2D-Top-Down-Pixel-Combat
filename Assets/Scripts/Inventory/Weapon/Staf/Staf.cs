using UnityEngine;

public class Staf : MonoBehaviour, IWeapon
{
    [SerializeField] private WeponInfoSO weaponInfo;
    [SerializeField] private GameObject prejectilePrefab;
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private float minYClampRight = -45f;
    [SerializeField] private float maxYClampRight = 45f;

    [SerializeField] private float maxYClampLeftUp = 45f;
    [SerializeField] private float minYClampLeftDown = 270f;

    private Animator animator;

    private const string FIRE_TRIGER = "Fire";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollow();
    }

    public void Attack()
    {
        animator.SetTrigger(FIRE_TRIGER);
    }


    public void SpawProjectileOnAnimEvent()
    {
        GameObject projectile = Instantiate(prejectilePrefab, spawnPosition.position, ActiveWeapon.Instanse.transform.rotation);
        projectile.GetComponent<MagicLaser>().UpdateLazerRange(weaponInfo.range);
        projectile.GetComponent<DamageDeal>().SetDamage(weaponInfo.damage);
    }

    public void MouseFollow()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositionInWorld.z = 0;

        Vector3 direction = mousePositionInWorld - PlayerControl.Instanse.transform.position;

        float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool flipX = mousePositionInWorld.x < PlayerControl.Instanse.transform.position.x;

        if (flipX)
        {
            angleZ = 180 - angleZ;
            if (angleZ >= 0f && angleZ <= 90f)
            {
                angleZ = Mathf.Clamp(angleZ, 0f, maxYClampLeftUp);
            }
            else if (angleZ <= 360f && angleZ >= 270f)
            {
                angleZ = Mathf.Clamp(angleZ, minYClampLeftDown, 360f);
            }
        }
        else
        {
            angleZ = Mathf.Clamp(angleZ, minYClampRight, maxYClampRight);
        }


        ActiveWeapon.Instanse.transform.rotation = Quaternion.Euler(0f, flipX ? 180 : 0, angleZ);
    }

    public WeponInfoSO GetWeaponInfoSo()
    {
        return weaponInfo;
    }
}
