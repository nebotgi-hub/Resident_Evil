using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
     public bool InRange = false;

    // tema camaras
    public Camera playerCamera;
    public Camera testCamera;
    public Animator doorAnimator;

    public bool isInCutscene = false;

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
        if (Input.GetButtonDown("Interact") && InRange && !isInCutscene)
        {
            // añadimos aqui que dependiendo que tag tenga la puerta, si o si debe cambiarse a camera test
            // validación de si poseemos el objeto que abre la llave
            StartCoroutine(TestCameraRoutine());
        }
    }

    IEnumerator TestCameraRoutine()
    {
        isInCutscene = true;

        // cambiar cameras activas
        testCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);

        Debug.Log("PUERTA BLOQUEADA → CAMARA TEST");

        // activar animacion
        doorAnimator.Play("doorAnimation", 0, 0f);
        yield return new WaitForSeconds(4.0f);

        // despus de los 5 seg, volvemos siempre a la camara main, esta mal, debe devolver a la que toca
        testCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);

        isInCutscene = false;
    }
}
