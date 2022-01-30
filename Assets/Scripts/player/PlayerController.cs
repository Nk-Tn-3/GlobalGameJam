using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] Animator chickenAnim, DinoAnim;
    private Animator playerAnimator;


    //Slope
    RaycastHit slopeHit;
    Vector3 slopeMoveDirection;

    [SerializeField] BoxCollider[] colliders;



    //ShapeShift
    [SerializeField] GameObject chicken,dino;
    public int index;


    //Cameras
    [SerializeField]GameObject dinoCam,chickecCam;

 
    //Stats
    [Space(5)]
    [Header("Stats")]
    [SerializeField] PlayerStats stats;

    public float currentHealth;

    bool canAttack = true;





    private void Start()
    {
        cameraTransform = Camera.main.transform;
        inputmanager = PlayerInputManager.Instance;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        collider = GetComponent<Collider>();
        moveSpeed = walkSpeed;
        playerAnimator = chickenAnim;
        stats.ChangeUIImage(index);
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius,groundLayer);
    
        ControlDrag();
        ShapeShift();
        if (index == 0)
        Jump();
        else
        {
            Attack();
        }
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
        if(horizontalInput.x* horizontalInput.x>0 || horizontalInput.y* horizontalInput.y>0 || inputmanager.GetMouseMove().x>1f && isGrounded)
        {
            //Walk
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
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
            playerAnimator.SetTrigger("Jump");
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


    void ShapeShift()
    {
        if (inputmanager.ShapeShift()){
            
            if (index == 0) index = 1;
            else { index = 0; }
       
        if (index == 1)
        {
                stats.ChangeUIImage(1);
            playerAnimator.SetTrigger("ShapeOut");
            StartCoroutine(Toggle(chicken,false));
            StartCoroutine(Toggle(dino, true));
            playerAnimator = DinoAnim;
                dinoCam.SetActive(true);
                chickecCam.SetActive(false);
                colliders[0].enabled = false;
                colliders[1].enabled = true;

        }
        else 
        {
                stats.ChangeUIImage(0);
                playerAnimator.SetTrigger("ShapeOut");
            StartCoroutine(Toggle(dino, false));
            StartCoroutine(Toggle(chicken, true));
            playerAnimator = chickenAnim;
                dinoCam.SetActive(false);
                chickecCam.SetActive(true);
                colliders[0].enabled = true;
                colliders[1].enabled = false;

            }
        }


    }
   
    void Attack()
    {
        if (index == 1 && inputmanager.Attack() && canAttack){

            playerAnimator.SetTrigger("Attack");
            canAttack = false;
            StartCoroutine(RefreshAttack());
        }
      
    }
    public float attackRecharge = 1.3f;
    IEnumerator RefreshAttack()
    {
        yield return new WaitForSeconds(attackRecharge);
        canAttack = true;

    }

   

    IEnumerator Toggle(GameObject obj,bool val)
    {
        yield return new WaitForSeconds(0.4f);
        obj.SetActive(val);
        yield return null;
    }




}
