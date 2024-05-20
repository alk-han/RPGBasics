using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class Item : MonoBehaviour
{
    // [SerializeField] private string itemName;
    // [SerializeField] private int quantity;
    // [SerializeField] private Sprite sprite;
    // [TextArea] [SerializeField] private string itemDescription;
    [SerializeField] private ItemSO itemSO;
    public int quantity;
    
    public string itemName; 
    public Sprite sprite;
    public GameObject itemPrefab;
    public string itemDescription;
    private InventoryManager inventoryManager;
    private bool isPickable;
    private Outline outline;

    private void Awake()
    {
        itemName = itemSO.itemName;
        sprite = itemSO.sprite;
        itemPrefab = itemSO.prefab;
        itemDescription = itemSO.itemDescription;
    }


    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void Update()
    {
        if (isPickable && Input.GetButtonDown("Interact"))
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, itemPrefab);

            if (leftOverItems <= 0) // No item left
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems; // Some quantity left
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.enabled = true;
            isPickable = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            outline.enabled = false;
            isPickable = false;
        }
    }
}
