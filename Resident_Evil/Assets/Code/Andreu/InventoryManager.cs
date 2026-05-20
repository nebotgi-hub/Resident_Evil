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

    // inicializacion del indice
    private int selectedIndex = 0;

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

        if (items.Count > 0)
        {
            SelectItem(0);
        }
    }

    private void Update()
    {
        if (items.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
    }

    void MoveRight()
    {
        if (items.Count == 0) return;

        selectedIndex++;
        if (selectedIndex >= items.Count)
            selectedIndex = 0;

        SelectItem(selectedIndex);
    }

    void MoveLeft()
    {
        if (items.Count == 0) return;

        selectedIndex--;
        if (selectedIndex < 0)
            selectedIndex = items.Count - 1;

        SelectItem(selectedIndex);
    }

    // Seleccionamos los objetos del tipo ObjectModular
    public void SelectItem(int index)
    {
        if (index < 0 || index >= items.Count) return;

        // fix para controlar el indice de donde esta, hay bug, quitas esto se rompe el inventario
        selectedIndex = index;

        // llamamos al UIIventory asociado desde la public para que muestre la descripcion del objeto y el icono
        uiInventory.ShowDescription(items[index].description);
        uiInventory.ShowItem(items[index]);

        // mover marco
        uiInventory.MoveSelector(index);
    }

}
