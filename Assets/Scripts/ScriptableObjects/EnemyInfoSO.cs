using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfoSO", menuName = "Scriptable Objects/EnemyInfoSO")]
public class EnemyInfoSO : ScriptableObject
{
    public int enemyHealth;
    public int enemyMealDamaged;
    public int enemyRangeDamage;

    public float enemyProjectileRange;
    public float enemyAttackRange;
    public float enemyAttackColdown;

    public bool isEnemyCanShoot;
}
