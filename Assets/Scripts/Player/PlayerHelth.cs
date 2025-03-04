using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHelth : Singlton<PlayerHelth>
{
    public static event Action OnPlayerDeath;
    public bool IsDead { get; private set; }

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackTrust = 10f;
    [SerializeField] private float invencibleTime = 1f;

    private Knocback knocback;
    private Flashing flash;
    private Slider healthSlider;
    private Animator animator;

    private int currentHealth;
    private bool isRecovery = false;

    private const string PLAYER_STRING = "HealthSlider";
    private const string DEATH_STRING = "Death";
    private const string TOWN_SCENE_MANAGER = "Town_Levl";

    protected override void Awake()
    {
        base.Awake();

        knocback = GetComponent<Knocback>();
        flash = GetComponent<Flashing>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;

        UpdateSliderHealth();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy && !isRecovery)
        {
            TakeDamage(enemy.GetEnemyInfo().enemyMealDamaged, collision.gameObject.transform);
            StartCoroutine(RecoveryRoutin());
        }
    }

    public void TakeDamage(int damge, Transform damageSourse)
    {
        StartCoroutine(flash.FlashingRoutin());
        knocback.GetKnocback(damageSourse, knockBackTrust);

        ScreenShakeManger.Instanse.ShakeScreen();
        currentHealth -= damge;

        UpdateSliderHealth();
        DetectDeath();
    }

    public void HealPlayer(int heal)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += heal;
        }

        UpdateSliderHealth();
    }

    private IEnumerator RecoveryRoutin()
    {
        isRecovery = true;
        yield return new WaitForSeconds(invencibleTime);
        isRecovery = false;
    }
    private void UpdateSliderHealth()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(PLAYER_STRING).GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0 && !IsDead)
        {
            currentHealth = 0;
            UpdateSliderHealth();
            Destroy(ActiveWeapon.Instanse.gameObject);
            IsDead = true;
            animator.SetTrigger(DEATH_STRING);
            OnPlayerDeath?.Invoke();
            StartCoroutine(DeathRoutin());
        }
    }

    private IEnumerator DeathRoutin()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        Stamina.Instanse.RefreshAllStamina();
        SceneManager.LoadScene(TOWN_SCENE_MANAGER);
    }
}
