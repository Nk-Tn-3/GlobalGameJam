using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    PlayerInputManager inputmg;
    [SerializeField]GameObject menue;
    bool isPause = false;
    private void Start()
    {
        inputmg = PlayerInputManager.Instance;
    }

    private void Update()
    {
        if (inputmg.Pause())
        {
            isPause = !isPause;
            if (isPause)
            {
                menue.SetActive(true);
           
                Time.timeScale = 0f;
                
            }
            else
            {
                menue.SetActive(false);
                Time.timeScale = 1f;
             
            }
        }
        ToggleCursor();

    

    }
    void ToggleCursor()
    {
       if(!isPause)
            Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
    }
}
