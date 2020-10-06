using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GameTableItem : MonoBehaviour
{
    public enum Type
    {
        None = 0,
        Bacon,
        Bread,
        Meat,
        Onion,
        Salad,
        Tomato
    }

    public GameObject InstantiatedPrefab { get; set; } = null;

    public GameTableItem(Type type)
    {
        ItemType = type;
    }

    public Type ItemType = Type.None;

}
