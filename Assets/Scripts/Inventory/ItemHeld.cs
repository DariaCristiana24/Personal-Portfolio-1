using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemHeld : MonoBehaviour
{
    [SerializeField]
    ItemSlot slot;  
    bool itemInHand = false;
    ItemController itemController;
    GameObject itemGameobjectInHand;

    [SerializeField]
    Transform orientation;
    [SerializeField]
    Transform enviroment;

    public PlayerAttack activeWeapon;

    UIManager uiManager;

    bool itemDroppped;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }



    void Update()
    {
        inputs();
        addItemtoHand();
    }

    void addItemtoHand()
    {
        if (slot.transform.childCount != 0 && !itemInHand && !itemDroppped )
        {

            itemController = slot.GetComponentInChildren<ItemController>();
            itemGameobjectInHand = Instantiate(itemController.Item.itemGameObject, transform.position, orientation.rotation, transform);
            if (itemGameobjectInHand.tag == "Weapon")
            {
                itemGameobjectInHand.transform.localScale = new Vector3(2, 2, 2);
                itemGameobjectInHand.transform.position += new Vector3(0, -0.5f, 0);
                activeWeapon = itemGameobjectInHand.GetComponentInChildren<PlayerAttack>();
                activeWeapon.enabled = true;
                

            }
            
            
                itemGameobjectInHand.GetComponent<Collider>().enabled = false;
            
            itemInHand = true;
            itemDroppped = false;
        }
        if (slot.transform.childCount == 0 && !itemDroppped)
        {
            destroyObjInHand();
        }
    }

    void inputs()
    {
        if (Input.GetKey(KeyCode.Q)&& !uiManager.Paused)
        {
            dropItem();
        }
    }

    void destroyObjInHand()
    {
        Destroy(itemGameobjectInHand);
        itemInHand = false;
    }

    void dropItem()
    {
        if (itemInHand)
        {
            itemDroppped = true;
            itemController = slot.GetComponentInChildren<ItemController>();
            Instantiate(itemController.Item.itemGameObject, transform.position + orientation.forward * 2 + Vector3.down/2, orientation.rotation, enviroment);
            GameObject currentItem = itemController.gameObject;
            Destroy(currentItem);
            InventoryManager.Instance.Remove(itemController.Item);
            destroyObjInHand();

        }
        else
        {
            itemDroppped = false;
        }
    }

}
