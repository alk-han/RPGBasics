using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Data
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField] private int maxNumberOfItems;
    
    // Item Slot
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    // Item Description
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private GameObject itemPrefab;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, GameObject itemPrefab)
    {
        // Check to see if the slot is already full
        if (isFull) return quantity;

        // Update Name
        this.itemName = itemName;

        // Update image
        this.itemSprite = itemSprite;
        itemImage.enabled = true;
        itemImage.sprite = itemSprite;

        // --- Update Item Prefab
        this.itemPrefab = itemPrefab;
        
        // Update Desc
        this.itemDescription = itemDescription;

        // Update quantity
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            // Return LeftOvers
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        // Update Quantity Text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        if (thisItemSelected) // second time click
        {
            bool usable = inventoryManager.UseItem(itemName);
            if (usable)
            {
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
            }
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;

            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        }
    }

    private void EmptySlot()
    {
        quantityText.enabled = false;
        // itemImage.sprite = emptySprite;
        itemImage.enabled = false;
        itemDescription = "";
        itemName = "";

        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = emptySprite;
        itemSprite = emptySprite;
    }
    
    private void OnRightClick()
    {
        GameObject itemToDrop = Instantiate(itemPrefab);
        Item newItem = itemToDrop.GetComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemPrefab = itemPrefab;
        newItem.itemDescription = itemDescription;

        GameObject player = GameObject.FindWithTag("Player");
        Vector3 dropPosition = player.transform.TransformPoint(player.transform.forward * 1.5f);
        
        itemToDrop.transform.position = dropPosition;

        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        if (this.quantity <= 0)
        {
            EmptySlot();
        }
    }
}
