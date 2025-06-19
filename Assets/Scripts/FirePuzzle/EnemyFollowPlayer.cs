using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    public NavMeshAgent enemy;
    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            //Debug.Log("Killed Player");
            player.transform.GetComponent<PlayerController>().isAlive = false;
        }
    }
}
