using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ObjectModular itemData;
    private bool canPick = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPick = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPick = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canPick && Input.GetKeyDown(KeyCode.E))
        {
            PickItem();
        }
    }

    // pillamos el item si estamos en la esfera con el collider
    void PickItem()
    {
        InventoryManager.instance.AddItem(itemData);

        // Debug.Log("Recogido: " + itemData.objectName);

        Destroy(gameObject);
    }
}
