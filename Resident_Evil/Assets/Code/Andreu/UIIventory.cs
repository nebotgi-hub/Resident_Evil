using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    public Image itemImageBig;
    public TMP_Text itemNameText;

    // seccion de usar o descartar, los dos botones del inventario que dan utilidad
    public Image useImage;
    public Image discardImage;

    public void Start()
    {
        itemImageBig.enabled = false;
    }

    public void Refresh(List<ObjectModular> items)
    {
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
        
    }

    public void HighlightAction(int index)
    {
        Debug.Log("HighlightAction llamado: " + index);

        useImage.color = (index == 0) ? Color.red : Color.black;
        discardImage.color = (index == 1) ? Color.red : Color.black;
    }

    public void ShowDescription(string desc)
    {
        descriptionText.text = desc;
    }

    public void ShowItem(ObjectModular item)
    {
        itemImageBig.enabled = true;

        itemImageBig.sprite = item.Icon;

        itemImageBig.sprite = item.Icon;
        itemNameText.text = item.objectName;
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
