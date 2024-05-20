using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlots;

    public ItemSO[] itemSOs;

    private void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }      
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                return usable;
            }
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, GameObject itemPrefab)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            // if the itemSlot is not full AND the slot has the same item as this one
            // OR if the slot completely empty
            if (!itemSlots[i].isFull && itemSlots[i].itemName == itemName || itemSlots[i].quantity == 0)
            {
                int leftOverItems = itemSlots[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemPrefab);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, itemPrefab);
                }
                return leftOverItems;
            }
        }

        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].selectedShader.SetActive(false);
            itemSlots[i].thisItemSelected = false;
        }
    }
}
