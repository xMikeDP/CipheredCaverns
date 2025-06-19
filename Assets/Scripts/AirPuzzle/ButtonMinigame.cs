using UnityEngine;

public class ButtonMinigame : MonoBehaviour {
    public GameObject puzzle;
    public int requiredPressedButtons = 4;
    public int activeButtonCount = 0;

    public void UpdateCount(bool signal) {
        if (signal) {
            activeButtonCount++;
        }
        else {
            activeButtonCount--;
        }

        if (activeButtonCount >= requiredPressedButtons) {
            if (puzzle.CompareTag("WallJumpPuzzle")) {
                GetComponent<DoorController>().OpenDoor();
            } else if (puzzle.CompareTag("BossFight")) {
                //Debug.Log("EEEEAAAAAAAAABB");
                Transform lightSource = transform.parent.Find("LightSource");
                lightSource.GetComponent<LightSource>().isActive = true;
            }
            
        } else if (activeButtonCount < requiredPressedButtons) {
            if (puzzle.CompareTag("BossFight")) {
                Transform lightSource = transform.parent.Find("LightSource");
                lightSource.GetComponent<LightSource>().isActive = false;
            }
        }
    }
}
