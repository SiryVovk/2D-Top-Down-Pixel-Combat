using UnityEngine;

public class StartBosFight : MonoBehaviour
{
    [SerializeField] private GameObject bos;
    [SerializeField] private GameObject barrierSprites;

    private Collider2D[] colliders;
    private GameObject bosContainer;

    private const string BOS_CONTAINER_STRING = "BosContainer";
    private const string UI_STRING = "UI_Canvas";

    private void Awake()
    {
        colliders = GetComponents<Collider2D>();
        bosContainer = GameObject.Find(UI_STRING)?.transform.Find(BOS_CONTAINER_STRING)?.gameObject;
    }

    private void OnEnable()
    {
        PlayerHelth.OnPlayerDeath += DeactivateContainer;
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

        bosContainer.SetActive(true);

        BosSlime bosSlime = bos.GetComponent<BosSlime>();
        bosSlime.enabled = true;
        barrierSprites.SetActive(true);
    }

    private void DeactivateContainer()
    {
        bosContainer.SetActive(false);
    }
}
