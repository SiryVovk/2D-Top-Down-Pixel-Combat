using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private ParticleSystem deathParticle;

    private Knocback knocback;
    private Flashing flashing;

    private int currentHealth;

    private void Awake()
    {
        knocback = GetComponent<Knocback>();
        flashing = GetComponent<Flashing>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knocback.GetKnocback(PlayerControl.Instanse.transform, 15f);
        StartCoroutine(HandleDamageRoutine());
    }

    private IEnumerator HandleDamageRoutine()
    {
        yield return StartCoroutine(flashing.FlashingRoutin());

        if (currentHealth <= 0)
        {
            PickUpsSpawner pickUpsSpawner = GetComponent<PickUpsSpawner>();

            pickUpsSpawner?.DropItem();
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
