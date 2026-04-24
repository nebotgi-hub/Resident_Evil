using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActive : MonoBehaviour
{
    public GameObject cam;

    CameraActive curr_ca;

    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player"))
        {
            cam.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.SetActive(false);
        }
    }
}
