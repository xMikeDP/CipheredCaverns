using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Material buttonActivated;
    public Material buttonDeactivated;
    
    public bool isActivated = false;
    
    public void ToggleButton() {
        isActivated = !isActivated;

        if (isActivated) {
            GetComponent<Renderer>().material = buttonActivated;
        }
        else {
            GetComponent<Renderer>().material = buttonDeactivated;
        }
        
        transform.parent.gameObject.GetComponent<ButtonMinigame>().UpdateCount(isActivated);
    }
}
