using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Card,
    Ingredient,
    Bone,
    Default
}
public abstract class ItemObject : ScriptableObject
{

    public GameObject prefab;
    public ItemType type;
    public string itemName;
    [TextArea(15, 20)] public string description;

}
