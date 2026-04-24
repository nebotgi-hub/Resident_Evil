using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
     public bool InRange = false;

    void OnCollisionEnter(Collider other)
    {
        Debug.Log("Algo ha entrado en el trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            InRange = true;
        }
    }

    void OnCollisionExit(Collider other)
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
            Debug.Log("PUERTA BLOQUEADA");
        }
    }
}
