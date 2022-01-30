using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public float attackDmg;

    EnemyRagdoll ragdoll;

    private void Start()
    {
        ragdoll = GetComponent<EnemyRagdoll>();
    }
    public void GetHit(float dmg,Vector3 pos)
    {
        health -= dmg;

        if (health <= 0) {
            StartCoroutine(Die(pos));
           
        }
    }

    IEnumerator Die(Vector3 pos)
    {
        ragdoll.RagdollActive(true,pos);
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
