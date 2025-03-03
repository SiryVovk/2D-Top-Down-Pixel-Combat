using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private float enemySpeed = 2f;

    private Rigidbody2D rb;
    private Knocback knocback;
    private SpriteRenderer spriteRenderer;
    private EnemyAI enemyAI;

    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knocback = GetComponent<Knocback>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyAI = GetComponent<EnemyAI>();
    }

    private void FixedUpdate()
    {
        if (!knocback.IsKnocBacking)
        {
            Vector2 movePlayerTo = rb.position + (moveDirection * enemySpeed * Time.fixedDeltaTime);
            rb.MovePosition(movePlayerTo);
        }

        if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void ChangeDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void StopMoving()
    {
        moveDirection = Vector2.zero;
    }
}
