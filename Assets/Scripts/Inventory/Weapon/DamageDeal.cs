using UnityEngine;

public class DamageDeal : MonoBehaviour
{
    private int damage = 1;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        BosSlime bosSlime = other.gameObject.GetComponent<BosSlime>();

        enemyHealth?.TakeDamage(damage);
        bosSlime?.TakeDamage(damage);
    }
}
