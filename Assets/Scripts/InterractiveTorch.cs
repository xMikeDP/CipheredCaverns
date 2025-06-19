using UnityEngine;

public class InterractiveTorch : MonoBehaviour
{
    public Material torchActivatedMaterial;
    public Material torchDeactivatedMaterial;
    private GameObject torchHead;
    private Light torchLight;
    public bool isActivated = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        torchHead = transform.parent.Find("TorchHead").gameObject;
        torchLight = torchHead.GetComponent<Light>();
    }

    public void ChangeTorchState() {
        isActivated = !isActivated;

        if (isActivated) {
            torchHead.GetComponent<Renderer>().material = torchActivatedMaterial;
            torchLight.enabled = true;
        }
        else {
            torchHead.GetComponent<Renderer>().material = torchDeactivatedMaterial;
            torchLight.enabled = false;
        }
    }
}
