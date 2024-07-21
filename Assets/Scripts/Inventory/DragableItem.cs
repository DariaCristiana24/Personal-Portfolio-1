using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    Image image;
    InventoryManager inventoryManager;
    bool hoveringOnObject = false;

    void Awake()
    {
        image = transform.Find("ItemIcon").GetComponent<Image>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
       // Debug.Log("begin drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        image.raycastTarget = false;

    }
    public void OnDrag(PointerEventData eventData)
    {
       // Debug.Log(" drag");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("end drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveringOnObject=true;


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveringOnObject = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && hoveringOnObject)
        {
            Debug.Log("attempt");
            transform.SetParent(inventoryManager.ItemHeldSlot.transform);
        }
    }
}

