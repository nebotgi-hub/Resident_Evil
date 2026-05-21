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
            SoundManager.Instance.PlaySFX("Locked",2);
            DialogueManager.Instance.StartDialogue(new string[] {
                "This door is locked; I'll need a key."
            });
        }
        else
        {
            if (!InCutscene)
            {
                SoundManager.Instance.PlaySFX("DoorsAnim",2);
                StartCoroutine(CameraRoutine());
            }
        }
    }

    IEnumerator CameraRoutine()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        doorAnimator.SetTrigger("PlayDoor");
        animCamera.gameObject.SetActive(true);
        InCutscene = true;
        yield return new WaitForSeconds(4f);
        InCutscene = false;
        player.position = NextPosition.position;
        animCamera.gameObject.SetActive(false);
    }
}
