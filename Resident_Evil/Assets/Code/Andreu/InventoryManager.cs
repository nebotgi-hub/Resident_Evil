using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // instancia de la clase
    public static InventoryManager instance;

    // Array de objeto ObjectModular script
    public List<ObjectModular> items = new List<ObjectModular>();

    // variable que hace referencia a el inventario directo
    public UIIventory uiInventory;

    // esto se ejecuta antes del start, sirve para cargar el inventario antes que el propio objeto como tal
    private void Awake()
    {
        instance = this;
    }

    // añadimos items de tipo ObjectModular
    public void AddItem(ObjectModular item)
    {
        items.Add(item);
        uiInventory.Refresh(items);
    }

    // Seleccionamos los objetos del tipo ObjectModular
    public void SelectItem(int index)
    {
        if (index < 0 || index >= items.Count) return;

        // llamamos al UIIventory asociado desde la public para que muestre la descripcion del objeto
        uiInventory.ShowDescription(items[index].description);
    }

}
