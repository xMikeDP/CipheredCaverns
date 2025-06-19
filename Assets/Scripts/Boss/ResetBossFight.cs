using UnityEngine;

public class ResetBossFight : MonoBehaviour
{
    public GameObject boss;
    private BossStats bossStats;
    
    public GameObject phase1;
    private BossFight bossFight;
    public GameObject phase2;
    
    public GameObject buttons;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossStats = boss.GetComponent<BossStats>();
        bossFight = phase1.GetComponent<BossFight>();
    }

    public void Reset() {
        ResetButtons();
        
        bossStats.Reset();
        bossStats.ResetPosition();

        bossFight.currentPhase = 1;
        Transform lightSource = phase2.transform.Find("LightSource");
        lightSource.GetComponent<LightSource>().isActive = false;
        
        CloseAllDoors();
    }

    public void ResetButtons() {
        foreach (Transform child in buttons.transform) {
            ButtonScript buttonScript  = child.GetComponent<ButtonScript>();
            if (buttonScript.isActivated) {
                buttonScript.ToggleButton();
            }
        }
    }

    private void CloseAllDoors() {
        ObjectShifter[] shiftDoors1 = phase1.GetComponents<ObjectShifter>();
        shiftDoors1[0].UnshiftObject();
        shiftDoors1[1].UnshiftObject();
        
        ObjectShifter[] shiftDoors2 = phase2.GetComponents<ObjectShifter>();
        shiftDoors2[0].UnshiftObject();
        shiftDoors2[1].UnshiftObject();
    }
}
