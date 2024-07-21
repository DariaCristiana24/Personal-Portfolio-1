using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryMenu;
    [SerializeField]
    GameObject inventoryIcon;
    [SerializeField]
    GameObject inventoryPing;
    [HideInInspector] public bool Paused;

    CameraMovement cameraMovement;


    // Start is called before the first frame update
    void Start()
    {
        cameraMovement = FindObjectOfType<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cameraMovement.cursorStateChange();
            if(!Paused) 
            {
                inventoryMenu.SetActive(true);
                inventoryIcon.SetActive(false);
                inventoryPing.SetActive(false);
                Paused = true;
            } 
            else
            {
                inventoryMenu.SetActive(false);
                inventoryIcon.SetActive(true);
                Paused = false;
            }
        }
    }
}
