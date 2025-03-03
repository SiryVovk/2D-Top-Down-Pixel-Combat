using System.Collections;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    [SerializeField] private Material flashingMat;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuratin = 0.2f;

    private Material baseMaterial;

    private void Awake()
    {
        baseMaterial = spriteRenderer.material;
    }

    public IEnumerator FlashingRoutin()
    {
        spriteRenderer.material = flashingMat;
        yield return new WaitForSeconds(flashDuratin);
        spriteRenderer.material = baseMaterial;
    }
}
