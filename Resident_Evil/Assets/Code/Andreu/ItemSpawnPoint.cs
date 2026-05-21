using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    public ObjectModular item;
    public GameObject itemPrefab;

    private GameObject spawnedObject;

    public void Start()
    {
        Spawn();    
    }

    public void Spawn()
    {
        // checkeo para no reventar el spawn
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }

        spawnedObject = Instantiate(itemPrefab, transform.position, transform.rotation);
    }

    public void Despawn()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }
    }
}
