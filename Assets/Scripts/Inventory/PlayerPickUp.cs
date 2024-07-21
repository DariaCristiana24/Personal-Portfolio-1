using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    // Start is called before the first frame update

    Camera _camera;
    public int distanceRaycast= 15;
    [SerializeField]
    LayerMask pickupMask;

    Item item;

    void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        Ray cameraRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, distanceRaycast, pickupMask))
        {
                if (Input.GetKeyDown(KeyCode.E) && InventoryManager.Instance.checkForSpace())
                {
                    item = hitInfo.collider.GetComponent<ItemController>().Item;
                    InventoryManager.Instance.Add(item);
                    Debug.Log("picked up " + item.itemName);
                    Destroy(hitInfo.collider.gameObject);
                }
    
        }
    }

}
