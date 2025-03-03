using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private GameObject destroyProjectileVFX;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private bool isEnemyBullet = false;

    private Vector2 startPosition;

    private float projectileRange = 10;
    private int damage = 1;


    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveForward();
        ChackDistanse();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
    }

    private void ChackDistanse()
    {
        if (Vector3.Distance(startPosition, transform.position) > projectileRange)
        {
            DestryArrow();
        }
    }

    private void DestryArrow()
    {
        Instantiate(destroyProjectileVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void UpdateRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateProjectileSpeed(float projectileSpeed)
    {
        this.projectileSpeed = projectileSpeed;
    }

    public void SetProjectileDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Indestructible indestructible = other.GetComponent<Indestructible>();
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        PlayerHelth playerHelth = other.GetComponent<PlayerHelth>();
        BosSlime bosSlime = other.GetComponent<BosSlime>();

        if (!other.isTrigger && (enemyHealth || indestructible || playerHelth || bosSlime))
        {
            if (playerHelth && isEnemyBullet)
            {
                playerHelth.TakeDamage(damage, transform);
                DestryArrow();
            }
            else if ((enemyHealth || bosSlime) && !isEnemyBullet)
            {
                enemyHealth?.TakeDamage(damage);
                bosSlime?.TakeDamage(damage);
                DestryArrow();
            }

            DestryArrow();
        }
    }
}
