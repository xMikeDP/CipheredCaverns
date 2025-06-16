using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    public int logs = 0;
    public int logsMaxCapacity = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetInventory() {
        logs = 0;
    }
}
