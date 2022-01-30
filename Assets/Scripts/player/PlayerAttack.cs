using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Collider col;


    float damage = 40f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destructable"))
        {
        
            other.GetComponent<BrickWall>().GetHit();
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Stats>().GetHit(damage,this.transform.position);
            
        }
    }



}
