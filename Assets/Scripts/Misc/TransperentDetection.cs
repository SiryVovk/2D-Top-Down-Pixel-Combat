using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransperentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float alphaTransperency = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;

    private SpriteRenderer sprite;
    private Tilemap tilemap;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>())
        {
            if (sprite)
            {
                StartCoroutine(FadeRoutin(sprite, fadeTime, sprite.color.a, alphaTransperency));
            }
            else if (tilemap)
            {
                StartCoroutine(FadeRoutin(tilemap, fadeTime, tilemap.color.a, alphaTransperency));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>())
        {
            if (sprite)
            {
                StartCoroutine(FadeRoutin(sprite, fadeTime, sprite.color.a, 1f));
            }
            else if (tilemap)
            {
                StartCoroutine(FadeRoutin(tilemap, fadeTime, tilemap.color.a, 1f));
            }
        }
    }

    private IEnumerator FadeRoutin(SpriteRenderer sprite, float fadeTime, float startValue, float targetTransperacy)
    {
        float elapseTime = 0;

        while (elapseTime < fadeTime)
        {
            elapseTime += Time.deltaTime;
            float newAlpfa = Mathf.Lerp(startValue, targetTransperacy, elapseTime / fadeTime);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, newAlpfa);
            yield return null;
        }
    }

    private IEnumerator FadeRoutin(Tilemap tile, float fadeTime, float startValue, float targetTransperacy)
    {
        float elapseTime = 0;

        while (elapseTime < fadeTime)
        {
            elapseTime += Time.deltaTime;
            float newAlpfa = Mathf.Lerp(startValue, targetTransperacy, elapseTime / fadeTime);
            tile.color = new Color(tile.color.r, tile.color.g, tile.color.b, newAlpfa);
            yield return null;
        }
    }
}
