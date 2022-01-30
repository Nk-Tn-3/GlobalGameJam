using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    [SerializeField]
    private int maxindex =1;
    public int index;
    [SerializeField] bool keyDown;
    public AudioSource audioSource;

    private void Start()
    {
       
    }

    private void Update()
    {
        /*
        float axis = Input.GetAxis(Axis.VERTICAL);
        if(axis != 0)
        {
            if (!keyDown)
            {
                if (axis > 0)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = maxindex;
                    }
                }
                else if (axis < 0)
                {
                    if (index < maxindex)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
    }
*/
    }
}
