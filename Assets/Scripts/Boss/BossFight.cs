using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossFight : MonoBehaviour
{
    private GameObject player;
    private PlayerController pc;

    public GameObject boss;
    private BossStats bossStats;

    public GameObject buttons;
    private ButtonMinigame buttonMinigame;
    
    public GameObject resetPosition;
    
    public bool gameActive = false;
    public bool gameAlreadyStarted = false;

    public int currentPhase = 1;

    public Text bossHealthText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();

        bossStats = boss.GetComponent<BossStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive && gameAlreadyStarted && currentPhase == 1) {
            UpdateText();
            CheckWinLossCondition();
        } else if (gameActive && !gameAlreadyStarted) {
            EnableText();
            gameAlreadyStarted = true;
            bossStats.canMove = true;
        } else if (bossStats.health > 0) {
            UpdateText();
        }
    }
    
    private void CheckWinLossCondition() {
        if (bossStats.health <= 5) {
            bossStats.canTakeDamage = false;
            Win();
        } else if (!pc.isAlive) {
            DisableText();
            Lose();
        }
    }

    private void Win() {
        Transform lightSource = transform.Find("LightSource");
        lightSource.GetComponent<LightSource>().isActive = false;
        bossStats.canMove = true;
        currentPhase = 2;
        gameActive = false;
        gameAlreadyStarted = false;
        OpenDoors();
    }

    private void Lose() {
        //Debug.Log("Lose");
        gameActive = false;
        gameAlreadyStarted = false;
        pc.isAlive = true;
        ResetRoom();
        
    }
    
    private void ResetRoom() {
        // foreach (Transform child in buttons.transform) {
        //     ButtonScript buttonScript  = child.GetComponent<ButtonScript>();
        //     if (buttonScript.isActivated) {
        //         buttonScript.ToggleButton();
        //     }
        // }
        //
        // bossStats.Reset();
        // bossStats.ResetPosition();
        transform.parent.GetComponent<ResetBossFight>().Reset();
        //CloseDoors();
        
        StartCoroutine(ResetPlayerPosition());
    }
    
    private IEnumerator ResetPlayerPosition() {
        pc.canMove = false;
        player.transform.position = resetPosition.transform.position;
        yield return new WaitForSeconds(0.1f);
        pc.canMove = true;
    }

    private void OpenDoors() {
        ObjectShifter[] shiftDoors = GetComponents<ObjectShifter>();
        shiftDoors[0].ShiftObject();
        shiftDoors[1].ShiftObject();
    }
    
    private void UpdateText() {
        bossHealthText.text = "Boss Health: " + bossStats.health;
    }

    private void EnableText() {
        UpdateText();
        bossHealthText.gameObject.SetActive(true);
    }

    private void DisableText() {
        bossHealthText.gameObject.SetActive(false);
    }
}
