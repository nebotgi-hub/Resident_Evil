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

    public void Refresh(List<ObjectModular> items)
    {
        foreach (Transform child in contentPanel)
        {
            // destrozamos objeto para iniciarlo otra vez
            Destroy(child.gameObject);
        }

        for (int i = 0; i < items.Count; i++)
        {
            int index = i;

            GameObject btn = Instantiate(buttonPrefab, contentPanel);
            btn.GetComponentInChildren<TMP_Text>().text = items[i].objectName;

            // evento on click, para seleccionar los items del inventario
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                InventoryManager.instance.SelectItem(index);
            });
        }
    }

    public void ShowDescription(string desc)
    {
        descriptionText.text = desc;
    }
}
