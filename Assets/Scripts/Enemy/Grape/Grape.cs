using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectile;
    [SerializeField] private EnemyInfoSO enemyInfoSO;

    private Animator anim;

    private const string ATTACK_ANIM_STRING = "Attack";

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        anim.SetTrigger(ATTACK_ANIM_STRING);
    }

    public void AttackAnimationTriger()
    {
        Instantiate(grapeProjectile, transform.position, Quaternion.identity);
    }
}
