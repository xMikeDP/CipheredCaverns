using Unity.Burst.Intrinsics;
using UnityEngine;

public class ObjectShifter : MonoBehaviour
{
    public GameObject objectToShift;
    public float shiftSpeed = 2f;
    //public float shiftDistance = 10f;
    //public bool X, nX, Y, nY, Z, nZ;
    private float initX, initY, initZ;
    public float offsetX, offsetY, offsetZ;
    private Vector3 initPos;
    private Vector3 targetPos;
    public bool isActivated;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initX = objectToShift.transform.position.x;
        initY = objectToShift.transform.position.y;
        initZ = objectToShift.transform.position.z;
        
        initPos = objectToShift.transform.position;
        targetPos = new Vector3(offsetX + initX, offsetY + initY, offsetZ + initZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated) {
            ShiftObject();
        }
        else {
            UnshiftObject();
        }
    }
    
    public void ShiftObject() {
        isActivated = true;
        objectToShift.transform.position = Vector3.MoveTowards(objectToShift.transform.position, targetPos, shiftSpeed * Time.deltaTime);
        //objectToShift.transform.position = targetPos;
    }

    public void UnshiftObject() {
        isActivated = false;
        objectToShift.transform.position = Vector3.MoveTowards(objectToShift.transform.position, initPos, shiftSpeed * Time.deltaTime);
        //objectToShift.transform.position = initPos;
    }
}
