using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private bool isStopMoving = false;

    private bool canAttack = true;

    private enum State
    {
        Roaming,
        Attacking
    };

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private Vector2 roamingPosition;

    private float timeRoaming = 0;
    private float changeDirectionTime = 2f;
    private float attackRange;
    private float attackColdown;

    private void Awake()
    {
        state = State.Roaming;
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start()
    {
        roamingPosition = GetRoamingPosition();
        Enemy enemy = GetComponent<Enemy>();
        attackRange = enemy.GetEnemyInfo().enemyAttackRange;
        attackColdown = enemy.GetEnemyInfo().enemyAttackColdown;
    }

    private void Update()
    {
        MovementStateControll();
    }

    private void MovementStateControll()
    {
        switch (state)
        {
            case State.Roaming:
                Roaming();
                break;
            case State.Attacking:
                Attacking();
                break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        enemyPathfinding.ChangeDirection(roamingPosition);

        if (Vector2.Distance(transform.position, PlayerControl.Instanse.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        if (timeRoaming > changeDirectionTime)
        {
            roamingPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerControl.Instanse.transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if (isStopMoving)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.ChangeDirection(roamingPosition);
            }

            StartCoroutine(TimeBeetwenAttack());
        }

    }

    private IEnumerator TimeBeetwenAttack()
    {
        yield return new WaitForSeconds(attackColdown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
