using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitControll : MonoBehaviour
{


    [SerializeField]
    Transform HitPoint;
    public void ToggleHit(int val)
    {
        if(val==1)
        HitPoint.gameObject.SetActive(true);
        else HitPoint.gameObject.SetActive(false);
    }
}
