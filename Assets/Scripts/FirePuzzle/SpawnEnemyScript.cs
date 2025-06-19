using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour {
    public GameObject enemy;
    public GameObject parentFolder;
    
    private GameObject enemySpawned;

    public void SpawnEnemy() {
        enemySpawned = Instantiate(enemy, transform.position, transform.rotation);
        enemySpawned.transform.parent = parentFolder.transform;
        //Debug.Log("SPAWNING ENEMY");
    }
}
