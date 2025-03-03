using System.Collections;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteOfLazer;
    [SerializeField] private CapsuleCollider2D lazerCollider;

    [SerializeField] private float lazerGrowzTime;

    private float lazerRange;

    private bool isGrowing = true;

    private void Start()
    {
        DamageDeal damageDeal = GetComponent<DamageDeal>();
        LazerFaceMouse();
    }

    public void UpdateLazerRange(float lazerRange)
    {
        this.lazerRange = lazerRange;
        StartCoroutine(IncreseLazerSizeRoutin());
    }

    private IEnumerator IncreseLazerSizeRoutin()
    {
        float timePased = 0;

        while (spriteOfLazer.size.x < lazerRange && isGrowing)
        {
            timePased += Time.deltaTime;
            float linearT = timePased / lazerGrowzTime;

            spriteOfLazer.size = new Vector2(Mathf.Lerp(1f, lazerRange, linearT), 1f);
            lazerCollider.size = new Vector2(Mathf.Lerp(1f, lazerRange, linearT), 1f);
            lazerCollider.offset = new Vector2(lazerCollider.size.x / 2, 0);
            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutin());
    }

    private void LazerFaceMouse()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositionInWorld.z = 0;

        Vector2 direction = transform.position - mousePositionInWorld;
        transform.right = -direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Indestructible>())
        {
            isGrowing = false;
        }
    }
}
