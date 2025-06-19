using System;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            //Debug.Log("Killed Player");
            other.transform.GetComponent<PlayerController>().isAlive = false;
            if (transform.CompareTag("Boss")) {
                transform.GetComponent<BossStats>().Reset();
                transform.GetComponent<BossStats>().ResetPosition();
            }
        }
    }
}
