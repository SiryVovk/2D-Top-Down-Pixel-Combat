using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private int burstCount;
    [SerializeField] private int projectilesPerBurst;
    [SerializeField] private float projectileSpeed;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startDistance = 0.1f;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool stagger;
    [Tooltip("Activate only with stagger")]
    [SerializeField] private bool oscillate;

    private EnemyInfoSO enemyInfoSO;

    private bool isShooting = false;


    private void OnValidate()
    {
        if (oscillate)
        {
            stagger = true;
        }
        if (!oscillate)
        {
            stagger = false;
        }
        if (projectilesPerBurst < 1)
        {
            projectilesPerBurst = 1;
        }
        if (burstCount < 1)
        {
            burstCount = 1;
        }
        if (projectileSpeed < 0.1f)
        {
            projectileSpeed = 0.1f;
        }
        if (startDistance < 0.1f)
        {
            startDistance = 0.1f;
        }
        if (timeBetweenBursts < 0.1f)
        {
            timeBetweenBursts = 0.1f;
        }
        if (restTime < 0.1f)
        {
            restTime = 0.1f;
        }
    }

    private void Start()
    {
        enemyInfoSO = GetComponent<Enemy>().GetEnemyInfo();
    }

    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(FireRoutin());
        }
    }

    private IEnumerator FireRoutin()
    {
        isShooting = true;
        float startAngel, currentAngle, angelStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngel, out currentAngle, out angelStep, out endAngle);

        if (stagger)
        {
            timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst;
        }

        for (int i = 0; i < burstCount; i++)
        {
            if (!oscillate)
            {
                TargetConeOfInfluence(out startAngel, out currentAngle, out angelStep, out endAngle);
            }

            if (oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out startAngel, out currentAngle, out angelStep, out endAngle);
            }
            else if (oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngel;
                startAngel = currentAngle;
                angelStep *= -1;
            }


            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 spawnPos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectiles projectiles))
                {
                    projectiles.SetProjectileDamage(enemyInfoSO.enemyRangeDamage);
                    projectiles.UpdateRange(enemyInfoSO.enemyProjectileRange);
                    projectiles.UpdateProjectileSpeed(projectileSpeed);
                }

                currentAngle += angelStep;

                if (stagger)
                {
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }
            }

            currentAngle = startAngel;
            yield return new WaitForSeconds(restTime);

        }


        isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 target = PlayerControl.Instanse.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = 0f;
        angleStep = 0f;
        if (angleSpread != 0)
        {
            float halfAngle = angleSpread / 2;
            angleStep = angleSpread / (projectilesPerBurst - 1);
            startAngle = targetAngle - halfAngle;
            endAngle = targetAngle + halfAngle;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
