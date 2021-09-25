using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public int X_START;
    public int Y_START;

    public int X_SPACE_BETWEEN_ITEMS;
    public int COLUMNS;
    public int Y_SPACE_BETWEEN_ITEMS;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();   
    }

    public void CreateDisplay()
    {
        for(int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            // @TODO: Remove this block of code from here, this is just to display the Object name and description when the object is selected in the inventory
            /*Text[] textList = inventory.GetComponentsInChildren<Text>();
            foreach (Text t in textList)
            {
                if (t.gameObject.name == "ItemName")
                {
                    t.text = inventory.container[i].item.name;
                }
                else
                {
                    t.text = inventory.container[i].item.description;
                }
            }*/
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % COLUMNS)), Y_START + ((-Y_SPACE_BETWEEN_ITEMS * (i/COLUMNS))), 0f);
    }

    public void UpdateDisplay()
    {
        for(int i = 0; i < inventory.container.Count; i++)
        {
            if (!itemsDisplayed.ContainsKey(inventory.container[i]))
            {
                var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                itemsDisplayed.Add(inventory.container[i], obj);
            }
        }
    }
}
