using System.Collections;
using UnityEngine;

public class KillEnemiesGame : MonoBehaviour
{
    public int timeGoal = 30;
    public int currentTime = 0;
    
    public GameObject enemy;
    public GameObject enemySpawnPoints;

    private int spawnPointCount = 0;
    private int randIndex = 0;

    public GameObject resetPosition;
    
    public bool gameActive = false;
    private bool gameAlreadyStarted = false;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPointCount = enemySpawnPoints.transform.childCount;
        Debug.Log("Child count: " + spawnPointCount);
    }

    // Update is called once per frame
    void Update() {
        if (gameActive && gameAlreadyStarted) {
            
            CheckWinLossCondition();
        } else if (gameActive && !gameAlreadyStarted) {
            gameAlreadyStarted = true;
            StartCoroutine(StartTimer());
            StartCoroutine(StartEnemySpawn());
        }
    }

    private IEnumerator StartTimer() {
        yield return new WaitForSeconds(1);
        while (gameActive) {
            currentTime++;
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator StartEnemySpawn() {
        yield return new WaitForSeconds(0.5f);
        while (gameActive) {
            randIndex = Random.Range(0, spawnPointCount);
            Debug.Log(randIndex);
            Transform spawnPoint = enemySpawnPoints.transform.GetChild(randIndex);
            Debug.Log(spawnPoint.name);
            SpawnEnemyScript spawnScript = spawnPoint.GetComponent<SpawnEnemyScript>();
            if (spawnScript != null) {
                Debug.Log("Spawning enemy on " + spawnPoint.name);
                spawnScript.SpawnEnemy();
            }
            else {
                Debug.LogError("No spawn script on " + spawnPoint.name);
            }
            // transform.GetChild(randIndex).GetComponent<SpawnEnemyScript>().SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void CheckWinLossCondition() {
        if (currentTime >= timeGoal) {
            Win();
        }
    }
    
    private void Win() {
        Debug.Log("Win");
        gameActive = false;
        gameAlreadyStarted = false;
        if (GetComponent<DoorController>() != null) {
            GetComponent<DoorController>().OpenDoor();
        }
    }
    
}
