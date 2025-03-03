using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BosSlime : MonoBehaviour
{
    [SerializeField] private GameObject slimePrefab;

    [SerializeField] private int maxHealth = 300;
    [SerializeField] private int damage = 3;
    [SerializeField] private float bosSpeed = 5f;
    [SerializeField] private float bosSpeedSecondPhazeIncIncrees = 2f;
    [SerializeField] private float timeBeetwenSlimeSpawn = 2f;

    private GameObject bosConteiner;
    private Slider slider;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Flashing flashing;

    private Vector3 targetPosition;

    private int currentHealth;
    private bool isSecondPhaze;
    private bool isTherdPhaze;
    private const string BOS_CONTEINER_STRING = "BosContainer";

    private void Start()
    {
        currentHealth = maxHealth;
        bosConteiner = GameObject.Find(BOS_CONTEINER_STRING);
        slider = bosConteiner.GetComponentInChildren<Slider>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        flashing = GetComponent<Flashing>();
        UpdateBosSlider();
    }

    private void FixedUpdate()
    {
        Vector2 direction = MoveToTarget();

        SetSpriteDirection(direction);
    }

    private Vector2 MoveToTarget()
    {
        Vector2 direction = (targetPosition - transform.position).normalized;

        rb.linearVelocity = direction * bosSpeed;

        if (Vector2.Distance(targetPosition, transform.position) < 1f)
        {
            rb.linearVelocity = Vector2.zero;
            SetTargetPosition();
        }

        return direction;
    }

    private void SetSpriteDirection(Vector2 direction)
    {
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void UpdateBosSlider()
    {
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        StartCoroutine(flashing.FlashingRoutin());
        UpdateBosSlider();
        DetectPhazes();
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            bosConteiner.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void DetectPhazes()
    {
        float bosPhasesStarts = (float)currentHealth / (float)maxHealth;

        if (bosPhasesStarts < 0.66 && !isSecondPhaze)
        {
            bosSpeed += bosSpeedSecondPhazeIncIncrees;
            isSecondPhaze = true;
        }

        if (bosPhasesStarts < 0.33 && !isTherdPhaze)
        {
            StartCoroutine(SpawnSlimesRputin());
            isTherdPhaze = true;
        }
    }

    private void SetTargetPosition()
    {
        targetPosition = PlayerControl.Instanse.transform.position;
    }

    private IEnumerator SpawnSlimesRputin()
    {
        while (true)
        {
            Instantiate(slimePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBeetwenSlimeSpawn);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHelth playerHelth = collision.GetComponent<PlayerHelth>();

        playerHelth?.TakeDamage(damage, transform);
    }
}
