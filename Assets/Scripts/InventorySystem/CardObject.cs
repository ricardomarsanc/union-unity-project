using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Object", menuName = "Inventory System/Items/Card")]
public class CardObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Card;
    }
}
