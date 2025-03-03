using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeponInfoSO weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform spawnPoint;

    private Animator animator;

    private static string FIRE_TRIGER = "Fire";

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
        GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, ActiveWeapon.Instanse.transform.rotation);
        Projectiles projectiles = arrow.GetComponent<Projectiles>();
        projectiles.UpdateRange(weaponInfo.range);
        projectiles.SetProjectileDamage(weaponInfo.damage); animator.SetTrigger(FIRE_TRIGER);
    }

    public void MouseFollow()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositionInWorld.z = 0;

        Vector3 playerPosition = PlayerControl.Instanse.transform.position;

        Vector3 direction = mousePositionInWorld - playerPosition;
        float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool flipY = mousePositionInWorld.x < playerPosition.x;

        if (flipY)
        {
            angleZ = 180 - angleZ;
        }

        ActiveWeapon.Instanse.transform.rotation = Quaternion.Euler(0f, flipY ? 180f : 0f, angleZ);
    }

    public WeponInfoSO GetWeaponInfoSo()
    {
        return weaponInfo;
    }
}
