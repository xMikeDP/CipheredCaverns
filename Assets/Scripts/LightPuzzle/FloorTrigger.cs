using System;
using UnityEngine;

public class FloorTrigger : MonoBehaviour {
    public GameObject door;
    public GameObject requiredObject;
    public int requiredCount = 3;

    private int count = 0;
    private string objectTag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectTag = requiredObject.tag;
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(objectTag)) {
            count++;
            CheckIfComplete();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag(objectTag)) {
            count--;
            CheckIfComplete();
        }
    }

    private void CheckIfComplete() {
        if (count >= requiredCount) {
            if (GetComponent<DoorController>() != null) {
                GetComponent<DoorController>().OpenDoor();
            } else if (GetComponent<ObjectShifter>() != null) {
                GetComponent<ObjectShifter>().ShiftObject();
            }
            
            //Debug.Log(objectTag + " is complete");
        }
        else {
            if (GetComponent<DoorController>() != null) {
                GetComponent<DoorController>().CloseDoor();
            } else if (GetComponent<ObjectShifter>() != null) {
                GetComponent<ObjectShifter>().UnshiftObject();
            }
            //Debug.Log(objectTag + " is incomplete");
        }
    }
}
