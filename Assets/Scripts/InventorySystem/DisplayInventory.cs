using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{

    public GameManager gm;
    private InventoryObject inventory;

    public Text InventoryEmptyText;

    public int X_START;
    public int Y_START;

    public int X_SPACE_BETWEEN_ITEMS;
    public int COLUMNS;
    public int Y_SPACE_BETWEEN_ITEMS;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        inventory = gm.PlayerInventory;

        CreateDisplay();

        InventoryEmptyText.gameObject.SetActive(inventory.container.Count == 0 ? true : false);
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
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % COLUMNS)), Y_START + ((-Y_SPACE_BETWEEN_ITEMS * (i/COLUMNS))), 0f);
    }

    public void UpdateDisplay()
    {
        if (itemsDisplayed.Count > 0 && InventoryEmptyText.gameObject.activeInHierarchy == true)
        {
            InventoryEmptyText.gameObject.SetActive(false);
        }

        for(int i = 0; i < inventory.container.Count; i++)
        {
            InventoryEmptyText.gameObject.SetActive(false);
            if (!itemsDisplayed.ContainsKey(inventory.container[i]))
            {
                var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                itemsDisplayed.Add(inventory.container[i], obj);
            }
        }
    }
}
