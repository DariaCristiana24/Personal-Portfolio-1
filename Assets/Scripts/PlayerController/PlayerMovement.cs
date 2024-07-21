using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    Rigidbody rb;
    [SerializeField]
    float speed ;
    [SerializeField]
    float walkingSpeed = 10f;
    [SerializeField]
    float runningSpeed = 20f;
    [SerializeField]
    float crouchSpeed = 5f;
    [SerializeField]
    float airSpeed = 0.5f;

    [SerializeField]
    LayerMask groundMask;
   // [SerializeField]
    float groundDist = 1.1f;
    [SerializeField]
    float groundDrag = 10;
    [SerializeField]
    float airDrag = 5;

    [SerializeField]
    float jumpPower = 10;

    [SerializeField]
    Transform playerModel;

    [SerializeField]
    Transform orientation;

    [SerializeField]
    GameObject playerModelStanding;

    [SerializeField]
    GameObject playerModelCrouched;

    CameraMovement cameraMovement;

    bool crouched = false;

    [SerializeField]
    Transform itemHeld;

    UIManager uiManager;

    PlayerAttack playerAttack;
    // Start is called before the first frame update
    void Start()
    {
        speed = walkingSpeed;
        rb = GetComponent<Rigidbody>();
        cameraMovement = FindObjectOfType<CameraMovement>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!uiManager.Paused)
        {

            Vector3 moveVector = orientation.forward * verticalInput + orientation.right * horizontalInput;


            if (IsGrounded())
            {
                rb.AddForce(moveVector * speed * 10f, ForceMode.Force);
            }
            else
            {
                rb.AddForce(moveVector * speed * 10f * airSpeed, ForceMode.Force);
            }
        }
    }

    private void Update()
    {
        if (!uiManager.Paused)
        {
            inputs();
            groundCheck();
            if (running && !crouched)
            {
                speed = runningSpeed;
            }
        }
    }



    void inputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && !crouched)
        {
            speed = runningSpeed;
        }
        else
        {
           if (speed > walkingSpeed)
            {
                speed -= 0.1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!crouched)
            {
                Crouch(true);
            }
            
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (crouched)
            {
                Crouch(false);
            }
        }
    }

    public bool IsGrounded()
    {
        
    
        if (Physics.Raycast(playerModel.position, new Vector3(0, -1, 0), out RaycastHit hit, groundDist, groundMask))
        {
            return true;
        }

        return false;
    }

    void groundCheck()
    {
        if (IsGrounded())
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    public void Jump()
    {
        if (IsGrounded()) 
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool running = false;

    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!running )
            {
                running = true;

            }
            else
            {
                running = false;
            }
        }
    }

    private void Crouch(bool crouchStatus)
    {
        if (crouchStatus)
        {
            crouched=true;
            Debug.Log("crouched");
            playerModelStanding.SetActive(false);
            playerModelCrouched.SetActive(true);
            cameraMovement.CrouchedCamera();
            speed = crouchSpeed;
            itemHeld.position -= new Vector3(0, 0.8f, 0);
        }
        else
        {
            crouched = false;
            Debug.Log("uncrouched");
            playerModelStanding.SetActive(true);
            playerModelCrouched.SetActive(false);
            cameraMovement.UnCrouchedCamera();
            speed = walkingSpeed;
            itemHeld.position += new Vector3(0, 0.8f, 0);
        }
    }

    public void CrouchController(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!crouched)
            {
                Crouch(true);

            }
            else
            {
                Crouch(false);
            }
        }
    }

    public void Attack()
    {
        playerAttack = itemHeld.GetComponentInChildren<PlayerAttack>();
        if (playerAttack)
        {
            playerAttack.Attack();
        }
    }
}
