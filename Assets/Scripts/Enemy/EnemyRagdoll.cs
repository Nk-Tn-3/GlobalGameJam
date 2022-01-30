using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyRagdoll : MonoBehaviour
{
  Animator anim;
  Rigidbody rb;
  Collider collider;
   Collider[] ragCols;
   Rigidbody[] rbs;
    Enemy enemScript;
    NavMeshAgent agent;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        enemScript = GetComponent<Enemy>();
        ragCols = GetComponentsInChildren<Collider>();
        rbs = GetComponentsInChildren<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        RagdollActive(false,Vector3.zero);
       
    }


    public void RagdollActive(bool active,Vector3 pos)
    {
   
        foreach (var collider in ragCols)
        {
            collider.enabled = active;
        
        }
        foreach(var rigb in rbs)
        {
            rigb.detectCollisions = active;
            rigb.isKinematic = !active;
         
        }
        enemScript.enabled = !active;
        anim.enabled = !active;
        rb.detectCollisions = !active;
        rb.isKinematic = !active;
        agent.enabled = !active;
        Vector3 dir = (this.transform.position - pos).normalized;
        rb.AddForce(dir * 10f, ForceMode.Impulse);
        collider.enabled = !active;
    

    }
}
