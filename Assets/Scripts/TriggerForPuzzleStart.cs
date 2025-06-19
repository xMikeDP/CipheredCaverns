using System;
using UnityEngine;

public class TriggerForPuzzleStart : MonoBehaviour
{
    public GameObject puzzle;
    
    private GameObject player;
    private PlayerController pc;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (puzzle.CompareTag("FirePlace")) {
                //Debug.Log("AAA");
                puzzle.GetComponent<FirePlaceScript>().gameActive = true;
            } else if (puzzle.CompareTag("KillEnemyPuzzle")) {
                //Debug.Log("BBB");
                puzzle.GetComponent<KillEnemiesGame>().gameActive = true;
            } else if (puzzle.CompareTag("CombinedFirePlace")) {
                //Debug.Log("CCC");
                puzzle.GetComponent<CombinedFireAndKillGame>().gameActive = true;
            } else if (puzzle.CompareTag("WallJumpPuzzle")) {
                //Debug.Log("DDD");
                player = GameObject.FindGameObjectWithTag("Player");
                pc = player.GetComponent<PlayerController>();
                pc.canWallJump = true;
            } else if (puzzle.CompareTag("EnableDashAbility")) {
                player = GameObject.FindGameObjectWithTag("Player");
                pc = player.GetComponent<PlayerController>();
                pc.canDash = true;
            } else if (puzzle.CompareTag("BossFight")) {
                puzzle.GetComponent<BossFight>().gameActive = true;
            } else if (puzzle.CompareTag("CombinedFirePlaceBoss")) {
                puzzle.GetComponent<CombinedFireAndKillGame>().gameActive = true;
            }
        }
    }
}
