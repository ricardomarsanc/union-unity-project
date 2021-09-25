using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{

    public InventoryManager inventoryManager;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryManager.isInventoryActive)
            {
                Debug.Log("Player controller working");
                inventoryManager.OpenInventory();
            }
            else
            {
                inventoryManager.CloseInventory();
            }
        }
    }
}
