using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Animator animator;

    public bool isInventoryActive = false;


    public void OpenInventory()
    {
        isInventoryActive = true;
        animator.SetBool("inventoryOpen", true);
    }

    public void CloseInventory()
    {
        isInventoryActive = false;
        animator.SetBool("inventoryOpen", false);
    }
}
