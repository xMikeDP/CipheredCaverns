using System;
using UnityEngine;

public class TriggerForPuzzleStart : MonoBehaviour
{
    public GameObject puzzle;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && puzzle.CompareTag("FirePlace")) {
            puzzle.GetComponent<FirePlaceScript>().gameActive = true;
        } else if (other.CompareTag("Player") && puzzle.CompareTag("KillEnemyPuzzle")) {
            puzzle.GetComponent<KillEnemiesGame>().gameActive = true;
        }
    }
}
