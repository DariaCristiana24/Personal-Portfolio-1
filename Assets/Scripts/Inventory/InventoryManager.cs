using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();
    List<GameObject> itemSlots = new List<GameObject>();

    public int slotsAmount = 16;

    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject InvetorySlot;
    public GameObject ItemHeldSlot;
    [SerializeField]
    GameObject inventoryPing;
    void Awake()
    {
        Instance = this;
        for (int i = 0; i < slotsAmount; i++)
        {
            GameObject obj = Instantiate(InvetorySlot, ItemContent);
            itemSlots.Add(obj);
        }
    }
  
    
    public void Add(Item item)
    {
        bool doOnce = false;
        foreach (var slot in itemSlots)
        {
            if (slot.transform.childCount == 0 && !doOnce)
            {
                inventoryPing.SetActive(true);
                items.Add(item);
                GameObject obj = Instantiate(InventoryItem, slot.transform);
                var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

                itemName.text = item.itemName;
                itemIcon.sprite = item.itemIcon;

                ItemController itemController = obj.GetComponent<ItemController>();
                itemController.Item = item; 


                doOnce = true;
            }
        }
    }

    public void Remove(Item item)
    {
        
        items.Remove(item);
    }

    public bool checkForSpace()
    {
        foreach (var slot in itemSlots)
        {
            if (slot.transform.childCount == 0)
            {
                return true;
            }
        }
        return false;
    }

}
