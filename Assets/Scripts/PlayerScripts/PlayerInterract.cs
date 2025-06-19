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
            if (hit.collider.CompareTag("PickableLog") && playerInventory.logs < playerInventory.logsMaxCapacity) {
                playerInventory.logs++;
                hit.collider.gameObject.SetActive(false);
            } else if (hit.collider.CompareTag("FirePlace") && playerInventory.logs > 0) {
                FirePlaceScript firePlace = hit.collider.gameObject.GetComponent<FirePlaceScript>();
                playerInventory.logs--;
                firePlace.IncreaseTimer();
            } else if ((hit.collider.CompareTag("CombinedFirePlace") || hit.collider.CompareTag("CombinedFirePlaceBoss")) && playerInventory.logs > 0) {
                CombinedFireAndKillGame firePlace = hit.collider.gameObject.GetComponent<CombinedFireAndKillGame>();
                playerInventory.logs--;
                firePlace.IncreaseTimer();
            } else if (hit.collider.CompareTag("Button")) {
                ButtonScript buttonScript = hit.collider.gameObject.GetComponent<ButtonScript>();
                buttonScript.ToggleButton();
            } else if (hit.collider.CompareTag("InfoBoard")) {
                DisplayInfo displayInfo = hit.collider.gameObject.GetComponent<DisplayInfo>();
                //int id = displayInfo.ID;
                displayInfo.ShowInfo();
            } else if (hit.collider.CompareTag("Secret")) {
                SecretScript secretScript = hit.collider.gameObject.GetComponent<SecretScript>();
                secretScript.ActivateSecret();
                StartCoroutine(secretScript.DisableText());
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
