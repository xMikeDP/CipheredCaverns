using System;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // private void OnTriggEnter(Collision other) {
    //     Destroy(gameObject);
    //     if (other.gameObject.CompareTag("Enemy")) {
    //         Debug.Log("EAEAEA" + other.gameObject.name);
    //         Destroy(other.gameObject);
    //     }
    // }
    private float maxDistance = 100f;
    private Vector3 startPosition;

    void Start() {
        startPosition = transform.position;
    }

    void Update() {
        if (Vector3.Distance(transform.position, startPosition) > maxDistance) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") && !other.CompareTag("PlayerCapsule")) {
            Destroy(gameObject);
            if (other.CompareTag("Enemy")) {
                Debug.Log("EAEAEA" + other.gameObject.name);
                Destroy(other.gameObject);
            }
        }
    }
}
