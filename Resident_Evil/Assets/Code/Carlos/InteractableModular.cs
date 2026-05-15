using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableModular : MonoBehaviour
{
     public bool InRange = false;

    // tema camaras
    public Camera playerCamera;
    public Camera animCamera;
    public Animator doorAnimator;

    public bool isInCutscene = false;
    public bool isLocked = true;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo ha entrado en el trigger: " + other.name);

        if (other.CompareTag("Player") && !isLocked)
        {
            InRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InRange = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && InRange)
        {
            if(this.name == "typewriter")
            {
                DialogueManager.Instance.StartDialogue(new string[] {
                    "It looks like an old typewriter.",
                    "It's too bad it's broken; I'm sure it would be very useful."
                });
            }

            else if (this.name == "Door" || this.name == "Door2")
            {
                DialogueManager.Instance.StartDialogue(new string[] {
                    "This door is locked; I'll need a key to get in."
                });
            }

            if (!isInCutscene)
            {
                StartCoroutine(CameraRoutine());
            }
        }
    }

    IEnumerator CameraRoutine()
    {
        isInCutscene = true;

        // cambiar cameras activas
        animCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);

        Debug.Log("PUERTA BLOQUEADA → CAMARA TEST");

        // activar animacion
        doorAnimator.Play("doorAnimation", 0, 0f);
        yield return new WaitForSeconds(4.0f);

        // despus de los 5 seg, volvemos siempre a la camara main, esta mal, debe devolver a la que toca
        animCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);

        isInCutscene = false;
    }
}
