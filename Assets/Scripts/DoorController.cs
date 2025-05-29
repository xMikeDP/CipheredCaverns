using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public float openSpeed = 2f;
    public float openDistance = 10f;
    private float initX, initY, initZ;
    private bool isOpen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initX = door.transform.position.x;
        initY = door.transform.position.y;
        initZ = door.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Init: " + initX + " - " + initY + " - " + initZ);
        //Debug.Log("Current: " + door.transform.position.x + " - " + door.transform.position.y + " - " + door.transform.position.z);
        if (isOpen) {
            OpenDoor();
        }
        else {
            CloseDoor();
        }
    }

    public void OpenDoor() {
        isOpen = true;
        door.transform.Translate(Vector3.forward * Time.deltaTime);
        if (door.transform.position.y < initY - openDistance) {
            door.transform.position = new Vector3(initX, initY - openDistance, initZ);
        }
    }

    public void CloseDoor() {
        isOpen = false;
        door.transform.Translate(Vector3.back * Time.deltaTime);
        if (door.transform.position.y > initY) {
            door.transform.position = new Vector3(initX, initY, initZ);
        }
    }
}
