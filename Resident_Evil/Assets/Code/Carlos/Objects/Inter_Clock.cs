using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Inter_Clock : Interactable
{

    public GameObject cam;

    protected override void OnInteract()
    {
        DialogueManager.Instance.StartDialogue(new string[] {
                    "That's strange; the clock isn't moving forward.",
                    "It looks like a part of the mechanism is missing."
                });
    }

    protected override void Update()
    {
        base.Update();
        if (cam.activeSelf)
        {
            if (!SoundManager.Instance.IsPlayingSFX("Clock", 2))
            {
                SoundManager.Instance.PlaySFX("Clock", 2);
            }
        }
    }
}
