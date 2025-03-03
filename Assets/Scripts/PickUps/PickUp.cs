using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private enum PickUpEnum
    {
        Coin,
        Health,
        Stamina
    }

    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private PickUpEnum pickUpEnum;

    [SerializeField] private float playerDistance = 5f;
    [SerializeField] private float acalerationSpeed = 0.5f;
    [SerializeField] private float hightY = 1.5f;
    [SerializeField] private float popDuration = 1f;

    private Rigidbody2D rb;
    private Vector3 moveDir;

    private float moveSpeed = 0f;
    private float distanceRange = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(PopRoutine());
    }

    private void Update()
    {
        SpeedOnPlayerDirection();
    }

    private void SpeedOnPlayerDirection()
    {
        Vector3 playerPos = PlayerControl.Instanse.transform.position;

        if (Vector2.Distance(transform.position, playerPos) < playerDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += acalerationSpeed;
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDir * moveSpeed * Time.deltaTime;
    }

    private IEnumerator PopRoutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x + Random.Range(-distanceRange, distanceRange), transform.position.y + Random.Range(distanceRange, distanceRange));

        float timeDuration = 0f;

        while (timeDuration < popDuration)
        {
            timeDuration += Time.deltaTime;
            float linerT = timeDuration / popDuration;
            float hightT = animationCurve.Evaluate(linerT);
            float hight = Mathf.Lerp(0, hightY, hightT);

            transform.position = Vector2.Lerp(startPos, endPos, linerT) + new Vector2(0f, hight);

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>())
        {
            DetectPickUp();
            Destroy(gameObject);
        }
    }

    private void DetectPickUp()
    {
        switch (pickUpEnum)
        {
            case PickUpEnum.Coin:
                EconomyManager.Instanse.UpdateCoinsNumber();
                break;
            case PickUpEnum.Health:
                PlayerHelth.Instanse.HealPlayer(1);
                break;
            case PickUpEnum.Stamina:
                Stamina.Instanse.RefreshStamina();
                break;
            default:
                break;
        }
    }
}
