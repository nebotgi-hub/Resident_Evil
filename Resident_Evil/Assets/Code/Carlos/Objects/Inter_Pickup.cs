using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inter_Pickup : Interactable
{
    public string itemName;

    protected override void OnInteract()
    {
        SoundManager.Instance.PlaySFX("pickup");
        // Inventory.Instance.AddItem(itemName);
        Destroy(gameObject);
    }
}
