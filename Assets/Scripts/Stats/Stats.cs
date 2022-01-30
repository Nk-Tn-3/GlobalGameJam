using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public float attackDmg;

  

    public void GetHit(float dmg)
    {
        health -= dmg;

        if (health <= 0) {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
