using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToogle : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject Cam;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && Time.timeScale != 0f && !Cam.activeSelf || Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);

            if (isOpen)
            {
                // pausar game
                Time.timeScale = 0f;
            }
            else
            {
                // reanudar game
                Time.timeScale = 1f;
            }
        }
    }
}
