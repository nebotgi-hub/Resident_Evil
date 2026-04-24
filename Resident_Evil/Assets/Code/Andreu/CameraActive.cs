using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActive : MonoBehaviour
{
    public GameObject cam;
    Transform player_tr;

    CameraActive curr_ca;

    private void Update()
    {
        // if (cam.activeSelf)
        // {
        //     cam.transform.LookAt(player_tr, Vector3.up);
        // }
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player"))
        {
            // player_tr = other.transform;
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
