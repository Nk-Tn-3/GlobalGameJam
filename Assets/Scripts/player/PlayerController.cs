using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Collider collider;
    private float moveSpeed;
    Vector3 moveDirection;
    PlayerInputManager inputmanager;


    [Header("Stats")]
    public float walkSpeed = 5f;
    public float runSpeed=10f;
    public float crouchSpeed=2f;
    public float acceleration = 1f;
    public float jumpHeight;

    float rbDrag = 6f;
    float airDrag = 1f;
    [SerializeField]
    float movementMultiplier = 10f;
    [SerializeField]
    float airmovementMultiplier = 0.1f;
    

    //GroundCheck
    bool isGrounded;
    bool isJumping;


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    float sphereRadius = 0.2f;
    float playerHeight = 2f;
    Vector2 horizontalInput;
    private Transform cameraTransform;


    //Slope
    RaycastHit slopeHit;
    Vector3 slopeMoveDirection;



  



    private void Start()
    {
        cameraTransform = Camera.main.transform;
        inputmanager = PlayerInputManager.Instance;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        collider = GetComponent<Collider>();
        moveSpeed = walkSpeed;
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius,groundLayer);
    
        ControlDrag();
        Jump();
        ControlSpeed();
    }



    private void FixedUpdate()
    {
        PlayerMove();

    }
    void PlayerMove()
    {
        horizontalInput = inputmanager.GetPlayerMovement();

        moveDirection = transform.forward * horizontalInput.y + transform.right * horizontalInput.x;
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
       else if (isGrounded && OnSlope())
        {
            moveInSlope();
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);

        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airmovementMultiplier, ForceMode.Acceleration);
        }
      
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = rbDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
        
    }

    void Jump()
    {

        if (inputmanager.PlayerJump() && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * Mathf.Sqrt(jumpHeight * -1f * Physics.gravity.y), ForceMode.Impulse);
      
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
              
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    void moveInSlope()
    {
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);   
    }





    //Sprinting
    void ControlSpeed()
    {
     
        if (inputmanager.IsSprinting() && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed,runSpeed, acceleration*Time.deltaTime);
        }
        else
        {
            moveSpeed = moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }
}
