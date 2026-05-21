using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inter_Door : Interactable
{
    public Animator doorAnimator;
    public Camera animCamera;
    public Transform NextPosition;

    public bool isLocked = false;
    public bool InCutscene = false;

    protected override void OnInteract()
    {
        if (isLocked)
        {
            ObjectModular key = InventoryManager.instance.items.Find(item => item.itemType == ObjectModular.ItemType.Key);

            if (key != null)
            {
                // tiene llave: abre la puerta y consume la llave
                isLocked = false;
                InventoryManager.instance.items.Remove(key);
                InventoryManager.instance.uiInventory.Refresh(InventoryManager.instance.items);

            }
            else
            {
                // no tiene llave
                SoundManager.Instance.PlaySFX("Locked", 2);
                DialogueManager.Instance.StartDialogue(new string[] {
                "This door is locked; I'll need a key."
            });
            }
        }
        else
        {
            if (!InCutscene)
            {
                SoundManager.Instance.PlaySFX("DoorsAnim", 2);
                StartCoroutine(CameraRoutine());
            }
        }
    }

    IEnumerator CameraRoutine()
    {
        GameObject player = GameObject.FindWithTag("Player");
        doorAnimator.SetTrigger("PlayDoor");
        animCamera.gameObject.SetActive(true);
        InCutscene = true;
        InventoryManager.instance.SaveInventory();
        yield return new WaitForSeconds(4f);
        InCutscene = false;
        player.transform.position = NextPosition.position;

        player.GetComponent<PlayerMovement>().SetCheckPoint(NextPosition);
        animCamera.gameObject.SetActive(false);
    }
}
