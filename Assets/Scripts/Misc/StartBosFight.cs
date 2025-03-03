using UnityEngine;

public class StartBosFight : MonoBehaviour
{
    [SerializeField] private GameObject sliderConteiner;
    [SerializeField] private GameObject bos;
    [SerializeField] private GameObject barrierSprites;

    private Collider2D[] colliders;

    private void Awake()
    {
        colliders = GetComponents<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerHelth playerHelth = collision.GetComponent<PlayerHelth>();

        if (playerHelth != null)
        {
            ActivateBos();
        }
    }

    private void ActivateBos()
    {
        foreach (Collider2D collider in colliders)
        {
            collider.isTrigger = false;
        }

        sliderConteiner.SetActive(true);

        BosSlime bosSlime = bos.GetComponent<BosSlime>();
        bosSlime.enabled = true;
        barrierSprites.SetActive(true);
    }
}
