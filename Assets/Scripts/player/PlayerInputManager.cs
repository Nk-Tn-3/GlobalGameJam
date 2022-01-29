
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private static PlayerInputManager _instance;

    public static PlayerInputManager Instance
    {
        get
        {
            return _instance;
        }
    }
    
    PlayerInput keyInputs;
    PlayerInput.PlayerMovementActions movementActions;

    Vector2 horizontalInput;

    private void Awake()
    {
        if(_instance!= null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        keyInputs = new PlayerInput();
        movementActions = keyInputs.PlayerMovement;
        movementActions.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        

    }
 public Vector2 GetPlayerMovement()
    {
        return horizontalInput;
    }
    public Vector2 GetMouseMove()
    {
        return movementActions.MouseLook.ReadValue<Vector2>();
    }

    public bool PlayerJump()
    {
        
        return movementActions.Jump.triggered;
    }

    public bool IsSprinting()
    {
        return movementActions.Run.ReadValue<float>()==1f;
    }
    public bool Attack()
    {
        return movementActions.Attack.triggered;
    }
    public bool ShapeShift()
    {
        return movementActions.ShapeShift.triggered;
    }

    private void OnEnable()
    {
        keyInputs.Enable();
    }
    private void OnDisable()
    {
        keyInputs.Disable();
    }
}
