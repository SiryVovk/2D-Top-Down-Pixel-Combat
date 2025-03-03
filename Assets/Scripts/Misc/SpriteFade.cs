using System.Collections;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.4f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public IEnumerator SlowFadeRoutin()
    {
        float elapsTime = 0f;
        float startValue = spriteRenderer.color.a;

        while (elapsTime < fadeTime)
        {
            elapsTime += Time.deltaTime;
            float newAlpfa = Mathf.Lerp(startValue, 0f, elapsTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpfa);

            yield return null;
        }

        Destroy(gameObject);
    }
}
