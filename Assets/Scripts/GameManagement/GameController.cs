using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = 60;
       


#endif
    }


  
    // Update is called once per frame
    void Update()
    {
        
    }
}
