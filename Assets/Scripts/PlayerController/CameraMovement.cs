using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Transform orientation;
    [SerializeField]
    Transform playerModel; 
    [SerializeField]
    Transform cameraPos;

    float xAxis;
    float yAxis;

    [SerializeField]
    float sensitivity = 5;

    [SerializeField]
    bool firstperson = true;

    Quaternion cameraRotation;
    float maxRotation = 89;

    [SerializeField]
    Vector3 offset = new Vector3(0, 0, -3f);

    [SerializeField]
    LayerMask wallMask;

    PlayerMovement player;

    Camera cam;
    Rigidbody playerRb;

    [SerializeField]
    Vector3 cameraCrouchOffset = new Vector3(0, 2, 0);

    bool CursorUnlocked;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = GetComponent<Camera>();
        player = FindObjectOfType<PlayerMovement>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
        inputs();

        if (!CursorUnlocked)
        {
            cameraMovement();
        }

    }

    void cameraMovement()
    {
        cameraRotation.x += -yAxis * Time.deltaTime * sensitivity * 100f;
        cameraRotation.y += xAxis * Time.deltaTime * sensitivity * 100f;

        if (firstperson)
        {
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, -maxRotation, maxRotation);
            firstPersonRotation();
        }
        else
        {
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, 0, maxRotation);
            thirdPersonRotation();
            if (IsWallRight())
            {
                playerRb.AddForce(-orientation.right, ForceMode.Impulse);
            }
            if (IsWallLeft())
            {
                playerRb.AddForce(orientation.right, ForceMode.Impulse);

            }

        }
    }

    void firstPersonRotation()
    {
        transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0);
        orientation.transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
        playerModel.transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
    }

    void thirdPersonRotation()
    {
        Quaternion rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0);
        transform.position = orientation.position + rotation * offset;
        transform.LookAt(orientation.position);

        playerModel.transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
        orientation.transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);

    }

    void inputs()
    {
        xAxis = Input.GetAxis("Mouse X");
        yAxis = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.F) && !CursorUnlocked)
        {
            CameraChange();
        }

        
    }

    public void CameraChange()
    {
        if (firstperson)
        {
            firstperson = false;
        }
        else
        {
            transform.position = cameraPos.position;
            firstperson = true;
        }
    }

    public bool IsWallRight()
    {

        Ray cameraRightRay = cam.ViewportPointToRay(new Vector3(0.99f, 0.5f, 0));

        if (Physics.Raycast(cameraRightRay, out RaycastHit hit, 1, wallMask))
        {
            return true;
        }
        return false;
    }
    public bool IsWallLeft()
    {

        RaycastHit hit;
        Ray cameraLeftRay = cam.ViewportPointToRay(new Vector3(0.01f, 0.5f, 0));
        if (Physics.Raycast(cameraLeftRay, out hit, 1, wallMask))
        {
            return true;
        }
        return false;
    }


    public void CrouchedCamera()
    {
        cameraPos.position -= cameraCrouchOffset;
    }
    public void UnCrouchedCamera()
    {
        cameraPos.position += cameraCrouchOffset;
    }

    public void cursorStateChange()
    {

        if (!CursorUnlocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CursorUnlocked = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            CursorUnlocked = false;
        }
    }
}
