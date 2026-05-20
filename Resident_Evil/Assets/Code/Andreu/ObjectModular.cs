using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// recordamos que he cambiado el monobehavior por esto, scriptable para hacerlo desde el project y ale
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/ObjectModular")]
public class ObjectModular : ScriptableObject
{
    // lo convierto todo en un script para que deje de ser un gameobject y meterle a cada gameobject este script
    // ahora cada vez que haga un itemPickUp, se llamará a esta lógica
    public enum ItemType
    {
        Key,
        Heal,
        Other
    };

    public string objectName;
    [TextArea(2, 5)]
    public string description;
    public Sprite Icon;
    public ItemType itemType;
}
