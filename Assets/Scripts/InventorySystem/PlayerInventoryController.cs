using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{

    public InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryManager.isInventoryActive)
            {
                inventoryManager.OpenInventory();
            }
            else
            {
                inventoryManager.CloseInventory();
            }
        }
    }
}
