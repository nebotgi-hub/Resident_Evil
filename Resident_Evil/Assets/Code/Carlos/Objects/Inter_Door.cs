using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inter_Door : Interactable
{
    public Animator doorAnimator;
    public Camera animCamera;
    public Transform NextPosition;

    public bool isLocked = false;

    protected override void OnInteract()
    {
        if (isLocked)
        {
            SoundManager.Instance.PlaySFX("Locked");
            DialogueManager.Instance.StartDialogue(new string[] {
                "This door is locked; I'll need a key."
            });
        }
        else
        {
            SoundManager.Instance.PlaySFX("DoorsAnim");
            StartCoroutine(CameraRoutine());
        }
    }

    IEnumerator CameraRoutine()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        doorAnimator.SetTrigger("PlayDoor");
        animCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        player.position = NextPosition.position;
        animCamera.gameObject.SetActive(false);
    }
}
