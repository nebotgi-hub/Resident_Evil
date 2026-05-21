using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inter_Typewriter : Interactable
{
    protected override void OnInteract()
    {
        DialogueManager.Instance.StartDialogue(new string[] {
                    "It looks like an old typewriter.",
                    "It's too bad it's broken; I'm sure it would be very useful."
                });
    }
}
