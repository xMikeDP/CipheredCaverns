using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KillEnemiesGame : MonoBehaviour
{
    public int timeGoal = 30;
    public int currentTime = 0;
    
    public GameObject enemy;
    public GameObject enemySpawnPoints;
    public GameObject enemyFolder;
    
    private GameObject player;
    private PlayerController pc;

    private int spawnPointCount = 0;
    private int randIndex = 0;

    public GameObject resetPosition;
    
    public bool gameActive = false;
    private bool gameAlreadyStarted = false;
    
    public Text timeLeftText;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        
        spawnPointCount = enemySpawnPoints.transform.childCount;
        //Debug.Log("Child count: " + spawnPointCount);
    }

    // Update is called once per frame
    void Update() {
        if (gameActive && gameAlreadyStarted) {
            UpdateText();
            CheckWinLossCondition();
        } else if (gameActive && !gameAlreadyStarted) {
            pc.canCastFireballs = true;
            gameAlreadyStarted = true;
            EnableText();
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
            //Debug.Log(randIndex);
            Transform spawnPoint = enemySpawnPoints.transform.GetChild(randIndex);
            SpawnEnemyScript spawnScript = spawnPoint.GetComponent<SpawnEnemyScript>();
            if (spawnScript != null) {
                spawnScript.SpawnEnemy();
            }
            else {
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void CheckWinLossCondition() {
        if (currentTime >= timeGoal) {
            DisableText();
            Win();
        } else if (!pc.isAlive) {
            DisableText();
            Lose();
        }
    }
    
    private void Win() {
        //Debug.Log("Win");
        gameActive = false;
        gameAlreadyStarted = false;
        
        foreach (Transform child in enemyFolder.transform) {
            Destroy(child.gameObject);
        }
        
        if (GetComponent<DoorController>() != null) {
            GetComponent<DoorController>().OpenDoor();
        }
    }

    private void Lose() {
        //Debug.Log("Lose");
        gameActive = false;
        gameAlreadyStarted = false;
        currentTime = 0;
        pc.isAlive = true;
        ResetRoom();
    }

    private void ResetRoom() {
        foreach (Transform child in enemyFolder.transform) {
            Destroy(child.gameObject);
        }
        
        StartCoroutine(ResetPlayerPosition());
    }
    
    private IEnumerator ResetPlayerPosition() {
        pc.canMove = false;
        player.transform.position = resetPosition.transform.position;
        yield return new WaitForSeconds(0.1f);
        pc.canMove = true;
    }
    
    private void UpdateText() {
        timeLeftText.text = (timeGoal - currentTime).ToString("0") + "s left!";
    }

    private void EnableText() {
        UpdateText();
        timeLeftText.gameObject.SetActive(true);
    }

    private void DisableText() {
        timeLeftText.gameObject.SetActive(false);
    }
}
