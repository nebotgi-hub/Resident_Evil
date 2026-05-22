using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIIventory : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject buttonPrefab;
    public TMP_Text descriptionText;

    // referencia a la transformacion del selector
    public RectTransform selectionFrame;

    private List<GameObject> spawnedButtons = new List<GameObject>();
    public List<RectTransform> slots;

    // seccion de la imagen grande y el nombre del item
    public GameObject states3;
    public GameObject states2;
    public GameObject states1;

    public GameObject selected1;
    public GameObject selected2;


    public TMP_Text itemNameText;

    // seccion de usar o descartar, los dos botones del inventario que dan utilidad
    public Image useImage;
    public Image discardImage;

    public PlayerMovement player;


    public void Refresh(List<ObjectModular> items)
    {
        itemNameText.text = "";
        descriptionText.text = "";
        selectionFrame.gameObject.SetActive(false);

        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // cleaneamos los botones spawneados para que vaya a inicio
        spawnedButtons.Clear();

        for (int i = 0; i < items.Count; i++)
        {
            int index = i;

            // creamos boton dentro de canvas
            GameObject btn = Instantiate(buttonPrefab, contentPanel);

            // añadimos el boton dentro
            spawnedButtons.Add(btn);

            // necesito llamar al inventoryButton para sacar y asignar icono que será el que se verá en el inventario
            InventoryButton ib = btn.GetComponent<InventoryButton>();

            ib.text.text = items[i].objectName;
            ib.iconImage.sprite = items[i].Icon;

            var tmp = btn.GetComponentInChildren<TextMeshProUGUI>(true);

            // checkeo que exista el TMP este si o si, y sino palante
            if (tmp == null)
            {
                Debug.LogError("NO EXISTE TMP");
                continue;
            }

            tmp.text = items[index].objectName;

            var button = btn.GetComponent<Button>();

            // igual con el botón del prefab
            if (button == null)
            {
                Debug.LogError("BOTONN NULO");
                continue;
            }

            button.onClick.AddListener(() =>
            {
                InventoryManager.instance.SelectItem(index);
            });
        }
    }

    // de momento nada
    public void Update()
    {
        switch (player.lifes)
        {
            case 3:
                states3.SetActive(true);
                states2.SetActive(false);
                states1.SetActive(false);
                break;

            case 2:
                states3.SetActive(false);
                states2.SetActive(true);
                states1.SetActive(false);
                break;

            case 1:
                states3.SetActive(false);
                states2.SetActive(false);
                states1.SetActive(true);
                break;
        }
    }

    public void HighlightAction(int index)
    {
        Debug.Log("HighlightAction llamado: " + index);

        GameObject inventoryUI = GameObject.Find("Use");

        if (inventoryUI != null && inventoryUI.activeSelf)
        {
            selected1.SetActive(index == 0);
            selected2.SetActive(index == 1);
        }
        else
        {
            selected1.SetActive(false);
            selected2.SetActive(false);
        }
    }

    public void ShowDescription(string desc)
    {
        descriptionText.text = desc;
    }

    public void ShowItem(ObjectModular item)
    {
        itemNameText.text = item.objectName;

        // enseñar use o discard si solo es consumible
        bool showActions = item.isConsumable;

        useImage.gameObject.SetActive(showActions);
        discardImage.gameObject.SetActive(showActions);

        if (!showActions)
        {
            selected1.gameObject.SetActive(false);
            selected2.gameObject.SetActive(false);
        }
    }

    // mover el selector
    public void MoveSelector(int index)
    {
        if (slots == null || slots.Count == 0)
            return;

        if (index < 0 || index >= slots.Count)
            return;

        selectionFrame.gameObject.SetActive(true);

        selectionFrame.position = slots[index].position;
    }
}
