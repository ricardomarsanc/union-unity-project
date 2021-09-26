using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject UI_TalkText;
    public GameObject UI_GrabItemText;
    public InventoryObject PlayerInventory;

    public GameManager instance;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // If removed, the items of the inventory will remain
        PlayerInventory.Clear();
    }
}
