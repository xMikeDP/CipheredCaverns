using System.Collections;
using UnityEngine;

public class FirePlaceScript : MonoBehaviour {
    public int timeGoal = 20;
    public int currentTime = 0;
    public int timeRemaining = 8;
    private int timeRemainingCopy;
    
    public GameObject logs;
    public GameObject resetPosition;
    
    public GameObject fireLowState;
    public GameObject fireMediumState;
    public GameObject fireHighState;
    
    public bool gameActive = false;
    private bool gameAlreadyStarted = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRemainingCopy = timeRemaining;
    }

    // Update is called once per frame
    void Update() {
        if (gameActive && gameAlreadyStarted) {
            CheckFireState();
            CheckWinLossCondition();
        } else if (gameActive && !gameAlreadyStarted) {
            gameAlreadyStarted = true;
            StartCoroutine(StartTimer());
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

    private void CheckWinLossCondition() {
        if (timeRemaining == 0) {
            Lose();
        } else if (currentTime >= timeGoal) {
            Win();
        }
    }

    private void Lose() {
        Debug.Log("Lose");
        gameActive = false;
        gameAlreadyStarted = false;
        currentTime = 0;
        timeRemaining = timeRemainingCopy;
        ResetRoom();
    }

    private void ResetRoom() {
        foreach (Transform child in logs.transform) {
            child.gameObject.SetActive(true);
        }

        ResetPlayerInventory();
        StartCoroutine(ResetPlayerPosition());
    }

    private void ResetPlayerInventory() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
        playerInventory.logs = 0;
    }
    
    private IEnumerator ResetPlayerPosition() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();
        if (pc != null) {
            pc.canMove = false;
        }
        player.transform.position = resetPosition.transform.position;
        yield return new WaitForSeconds(0.1f);
        if (pc != null) {
            pc.canMove = true;
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
