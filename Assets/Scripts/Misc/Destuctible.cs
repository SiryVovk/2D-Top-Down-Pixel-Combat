using UnityEngine;

public class Destuctible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFXEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DamageDeal>())
        {
            PickUpsSpawner pickUpsSpawner = GetComponent<PickUpsSpawner>();
            pickUpsSpawner?.DropItem();
            Instantiate(destroyVFXEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
