using UnityEngine;

public class BossStats : MonoBehaviour {
    public int health = 20;
    private int healthCopy;
    public bool canTakeDamage = false;
    public bool canMove = false;
    
    public Vector3 spawnPosition;

    void Start() {
        healthCopy = health;
        spawnPosition = transform.position;
    }
    
    public void Reset() {
        health = healthCopy;
        canMove = false;
        canTakeDamage = false;
    }

    public void ResetPosition() {
        transform.position = spawnPosition;
    }

    public void TakeDamage() {
        if (canTakeDamage) {
            health--;
        }
    }
}