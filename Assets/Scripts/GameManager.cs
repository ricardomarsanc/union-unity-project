using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject UI_TalkText;
    public InventoryObject PlayerInventory;

    private void Start()
    {
        // @TODO: Uncomment this to reset the Player Inventory each game
        // PlayerInventory = new InventoryObject();
    }
}
