using System.Collections.Generic;
using UnityEngine;

public class PickUpsSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> pickUpsToSpawn;


    public void DropItem()
    {
        int randomSpawn = Random.Range(0, pickUpsToSpawn.Count);

        Instantiate(pickUpsToSpawn[randomSpawn], transform.position, Quaternion.identity);
    }
}
