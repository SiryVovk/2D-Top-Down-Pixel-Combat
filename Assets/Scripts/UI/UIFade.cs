using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singlton<UIFade>
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 1f;

    private IEnumerator fadeRoutin;

    public void FadeToBlack()
    {
        if (fadeRoutin != null)
        {
            StopCoroutine(fadeRoutin);
        }

        fadeRoutin = FadeRoutin(1);
        StartCoroutine(fadeRoutin);
    }

    public void FadeToClear()
    {
        if (fadeRoutin != null)
        {
            StopCoroutine(fadeRoutin);
        }

        fadeRoutin = FadeRoutin(0);
        StartCoroutine(fadeRoutin);
    }

    private IEnumerator FadeRoutin(float targetFade)
    {
        while (!Mathf.Approximately(fadeImage.color.a, targetFade))
        {
            float alpha = Mathf.MoveTowards(fadeImage.color.a, targetFade, fadeSpeed * Time.deltaTime);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
    }
}
