using UnityEngine;

public class PlayerInterract : MonoBehaviour {
    public float range = 3f;
    private PlayerInventory playerInventory;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        playerInventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            InterractWithObject();
        }
    }

    private void InterractWithObject() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range)) {
            if (hit.collider.tag == "PickableLog" && playerInventory.logs < playerInventory.logsMaxCapacity) {
                playerInventory.logs++;
                hit.collider.gameObject.SetActive(false);
            } else if (hit.collider.tag == "FirePlace" && playerInventory.logs > 0) {
                FirePlaceScript firePlace = hit.collider.gameObject.GetComponent<FirePlaceScript>();
                playerInventory.logs--;
                firePlace.IncreaseTimer();
            } 
        }
    }
}
