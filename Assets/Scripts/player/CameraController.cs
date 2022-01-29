using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerInputManager inputManager;

    //Settings part
    GameSettings settings;
    float mouseSensitivity;
    public Quaternion nextRotation;
    float YRotation;
    public float rotationLerp = 0.5f;
    public GameObject followTransform;
    private void Start()
    {
        inputManager = PlayerInputManager.Instance;
        settings = GameSettings.Instance;
        
       

    }

    private void FixedUpdate()
    {
        RotateCharacter();
    }

    void RotateCharacter()
    {
        mouseSensitivity = settings.mouseSensitivity * settings.mouseMultiplier;
        Vector2 mouseMoveDelta = inputManager.GetMouseMove();


        transform.rotation *= Quaternion.AngleAxis(mouseMoveDelta.x * mouseSensitivity, Vector3.up);



        followTransform.transform.rotation *= Quaternion.AngleAxis(mouseMoveDelta.y * mouseSensitivity, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }
      

        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);


    }
}
