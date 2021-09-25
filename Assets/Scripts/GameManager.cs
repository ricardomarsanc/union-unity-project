using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject UI_TalkText;
    public GameObject UI_GrabItemText;
    public InventoryObject PlayerInventory;

    private void Awake()
    {
        // If removed, the items of the inventory will remain
        PlayerInventory.Clear();
    }
}
