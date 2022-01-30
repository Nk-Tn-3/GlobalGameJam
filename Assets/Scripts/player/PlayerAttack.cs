using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Collider col;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destructable"))
        {
            print("attack");
            other.GetComponent<BrickWall>().GetHit();
        }
    }



}
