using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    public int logs = 0;
    public int logsMaxCapacity = 1;
    
    public void ResetInventory() {
        logs = 0;
    }
}
