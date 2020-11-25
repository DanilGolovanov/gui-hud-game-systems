using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public int xStart;
    public int yStart;

    public int xSpaceBetweenItems;
    public int numberOfColumns;
    public int ySpaceBetweenItems;
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

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                var obj = CreateInventoryItem(i);
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            CreateInventoryItem(i);
        }
    }

    private GameObject CreateInventoryItem(int index)
    {
        var obj = Instantiate(inventory.Container[index].item.prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(index);
        // format with commas to make it look better when there are more than 1k items
        obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[index].amount.ToString();
        return obj;
    }

    private Vector3 GetPosition(int index)
    {
        return new Vector3(xStart + (xSpaceBetweenItems * (index % numberOfColumns)), yStart + ((-ySpaceBetweenItems * (index / numberOfColumns))), 0f);
    }
}
