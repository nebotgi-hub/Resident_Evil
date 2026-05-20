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
            Destroy(child.gameObject);
        }

        for (int i = 0; i < items.Count; i++)
        {
            int index = i;

            GameObject btn = Instantiate(buttonPrefab, contentPanel);

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

    public void ShowDescription(string desc)
    {
        descriptionText.text = desc;
    }
}
