using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CombinedFireAndKillGame : MonoBehaviour
{
    public int timeGoal = 30;
    public int currentTime = 0;
    public int timeRemaining = 8;
    private int timeRemainingCopy;
    
    public GameObject enemySpawnPoints;
    public GameObject enemyFolder;
    
    public GameObject logs;
    
    public GameObject fireLowState;
    public GameObject fireMediumState;
    public GameObject fireHighState;
    
    private GameObject player;
    private PlayerController pc;

    public GameObject boss;
    private BossStats bossStats;

    private int spawnPointCount = 0;
    private int randIndex = 0;

    public GameObject resetPosition;
    
    public bool gameActive = false;
    private bool gameAlreadyStarted = false;
    
    public Text timeLeftText;
    public Text campfireTimeLeftText;
    public Text bossHealthText;
    
    private bool alreadyCompleted = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        
        spawnPointCount = enemySpawnPoints.transform.childCount;
        //Debug.Log("Child count: " + spawnPointCount);

        if (transform.CompareTag("CombinedFirePlaceBoss")) {
            bossStats = boss.GetComponent<BossStats>();
        }
        
        timeRemainingCopy = timeRemaining;
    }

    // Update is called once per frame
    void Update() {
        if (gameActive && gameAlreadyStarted) {
            UpdateText();
            CheckFireState();
            CheckWinLossCondition();
        } else if (gameActive && !gameAlreadyStarted) {
            gameAlreadyStarted = true;
            EnableText();
            StartCoroutine(StartTimer());
            StartCoroutine(StartEnemySpawn());
        }
        // } else if (bossStats.health > 0) {
        //     UpdateText();
        // }
        
        if (transform.CompareTag("CombinedFirePlaceBoss")) {
            if (bossStats.health <= 0 && !alreadyCompleted) {
                alreadyCompleted = true;
                DisableText();
                Destroy(boss);
                OpenDoors();
            }
        }
    }

    private IEnumerator StartTimer() {
        yield return new WaitForSeconds(1);
        while (gameActive) {
            currentTime++;
            timeRemaining--;
            yield return new WaitForSeconds(1);
        }
    }
    
    public void IncreaseTimer() {
        timeRemaining += 5;
    }
    
    private void CheckFireState() {
        if (timeRemaining <= 0) {
            fireLowState.SetActive(false);
            fireMediumState.SetActive(false);
            fireHighState.SetActive(false);
        } else if (timeRemaining <= 5) {
            fireLowState.SetActive(true);
            fireMediumState.SetActive(false);
            fireHighState.SetActive(false);
        } else if (timeRemaining <= 10) {
            fireLowState.SetActive(false);
            fireMediumState.SetActive(true);
            fireHighState.SetActive(false);
        } else if (timeRemaining > 10) {
            fireLowState.SetActive(false);
            fireMediumState.SetActive(false);
            fireHighState.SetActive(true);
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
            DisableText();
            Win();
        } else if (!pc.isAlive || timeRemaining == 0) {
            DisableText();
            Lose();
        }
    }
    
    private void Win() {
        Debug.Log("Win");
        gameActive = false;
        gameAlreadyStarted = false;
        
        foreach (Transform child in enemyFolder.transform) {
            Destroy(child.gameObject);
        }
        
        if (transform.CompareTag("CombinedFirePlace")) {
            DoorController[] doors = GetComponents<DoorController>();
            doors[0].OpenDoor();
            doors[1].OpenDoor();
        }

        if (transform.CompareTag("CombinedFirePlaceBoss")) {
            Transform lightSource = transform.parent.Find("LightSource");
            lightSource.GetComponent<LightSource>().isActive = true;
        }
    }

    private void Lose() {
        Debug.Log("Lose");
        gameActive = false;
        gameAlreadyStarted = false;
        currentTime = 0;
        alreadyCompleted = false;
        pc.isAlive = true;
        timeRemaining = timeRemainingCopy;
        ResetRoom();
    }

    private void ResetRoom() {
        foreach (Transform child in enemyFolder.transform) {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in logs.transform) {
            child.gameObject.SetActive(true);
        }

        if (transform.CompareTag("CombinedFirePlaceBoss")) {
            transform.parent.parent.GetComponent<ResetBossFight>().Reset();
            //CloseDoors();
        }
        
        ResetPlayerInventory();
        
        StartCoroutine(ResetPlayerPosition());
    }
    
    private void ResetPlayerInventory() {
        PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
        playerInventory.logs = 0;
    }
    
    private IEnumerator ResetPlayerPosition() {
        pc.canMove = false;
        player.transform.position = resetPosition.transform.position;
        yield return new WaitForSeconds(0.1f);
        pc.canMove = true;
    }
    
    private void OpenDoors() {
        ObjectShifter[] shiftDoors = transform.parent.GetComponents<ObjectShifter>();
        shiftDoors[0].ShiftObject();
        shiftDoors[1].ShiftObject();
    }

    private void UpdateText() {
        timeLeftText.text = (timeGoal - currentTime).ToString("0") + "s left!";
        campfireTimeLeftText.text = timeRemaining.ToString("0") + "s left until campfire burns out.";

        // if (transform.CompareTag("CombinedFirePlaceBoss")) {
        //     bossHealthText.text = "Boss Health: " + bossStats.health.ToString("0");
        // }
    }

    private void EnableText() {
        UpdateText();
        timeLeftText.gameObject.SetActive(true);
        campfireTimeLeftText.gameObject.SetActive(true);
        
        if (transform.CompareTag("CombinedFirePlaceBoss")) {
            bossHealthText.gameObject.SetActive(true);
        }
    }

    private void DisableText() {
        timeLeftText.gameObject.SetActive(false);
        campfireTimeLeftText.gameObject.SetActive(false);
        
        if (transform.CompareTag("CombinedFirePlaceBoss") && alreadyCompleted) {
            bossHealthText.gameObject.SetActive(false);
        }
    }
}
