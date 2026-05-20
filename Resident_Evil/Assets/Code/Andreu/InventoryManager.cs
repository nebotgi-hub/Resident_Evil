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

    public PlayerMovement player;

    private int actionIndex = 0;

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

        // movimiento
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            actionIndex = 0;
            // añadimos el indice de acción para decidir que hacer con el item
            uiInventory.HighlightAction(actionIndex);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            actionIndex = 1;
            uiInventory.HighlightAction(actionIndex);
        }

        // usabilidad de los items
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteAction();
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

    void ExecuteAction()
    {
        if (items.Count == 0) return;

        ObjectModular item = items[selectedIndex];

        // recordar, 0 usar, 1 descartar, esto con enum estaria de locos
        if (actionIndex == 0)
        {
            UseItem(item);
        }
        else if (actionIndex == 1)
        {
            // solo borrar los items consumibles
            if (item.isConsumable)
            {
                items.RemoveAt(selectedIndex);
                uiInventory.Refresh(items);
                SelectItem(0);
            }
            
        }
    }

    void UseItem(ObjectModular item)
    {
        if (item.itemType == ObjectModular.ItemType.Heal)
        {
            player.HealLife(3);

            if (item.isConsumable)
            {
                items.RemoveAt(selectedIndex);
                uiInventory.Refresh(items);

                ClampSelectionAfterRemove();
            }
        }
        else if (item.itemType == ObjectModular.ItemType.Key)
        {
            // checkeo de llave
            Debug.Log("Llave");
        }
        else
        {
            // por si habian mas tipos de objetos
            Debug.Log("No se puede usar");
        }
    }

    // esto fixea el indice de seleccion
    void ClampSelectionAfterRemove()
    {
        if (items.Count == 0)
        {
            selectedIndex = 0;
            return;
        }

        selectedIndex = Mathf.Clamp(selectedIndex, 0, items.Count - 1);

        // buscamos otro objecto para el marco, realmente un slot
        uiInventory.MoveSelector(selectedIndex);
        uiInventory.ShowItem(items[selectedIndex]);
        uiInventory.ShowDescription(items[selectedIndex].description);
    }
}
