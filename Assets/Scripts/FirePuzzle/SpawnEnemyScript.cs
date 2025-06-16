using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour {
    public GameObject enemy;

    public void SpawnEnemy() {
        Instantiate(enemy, transform.position, transform.rotation);
        Debug.Log("SPAWNING ENEMY");
    }
}
