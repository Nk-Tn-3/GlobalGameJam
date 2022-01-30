using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float radius = 1f;
    [SerializeField] private float distance = 2f; 
    public float damage=10f;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position)<= distance)
        {
            player.GetComponent<PlayerStats>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
