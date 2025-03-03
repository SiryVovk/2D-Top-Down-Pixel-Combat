using UnityEngine;

public class GrapeSplash : MonoBehaviour
{
    [SerializeField] private EnemyInfoSO enemyInfoSO;

    [SerializeField] private float colliderDisableTime = 0.4f;

    private SpriteFade spriteFade;

    private void Awake()
    {
        spriteFade = GetComponent<SpriteFade>();
    }

    private void Start()
    {
        StartCoroutine(spriteFade.SlowFadeRoutin());

        Invoke("DisableCollider", colliderDisableTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHelth playerHelth = collision.gameObject.GetComponent<PlayerHelth>();

        playerHelth?.TakeDamage(enemyInfoSO.enemyRangeDamage, transform);
    }

    private void DisableCollider()
    {
        Collider2D collider = GetComponent<Collider2D>();

        collider.enabled = false;
    }
}
