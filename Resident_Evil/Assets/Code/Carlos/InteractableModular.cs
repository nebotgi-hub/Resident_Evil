using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableModular : MonoBehaviour
{
     public bool InRange = false;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo ha entrado en el trigger: " + other.name);

        if (other.CompareTag("Player"))
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
        }
    }
}
