using System;
using UnityEngine;

public class Fireball : MonoBehaviour
{
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
                //Debug.Log("EAEAEA" + other.gameObject.name);
                Destroy(other.gameObject);
            } else if (other.CompareTag("Boss")) {
                other.transform.GetComponent<BossStats>().TakeDamage();
            }
        }
    }
}
