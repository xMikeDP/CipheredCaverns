using UnityEngine;
using UnityEngine.InputSystem;

public class LightReflector : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    float rotationSpeed = 0.2f;
    
    Vector3 position;
    Vector3 direction;
    LineRenderer lineRenderer;
    
    public bool isOpen;
    GameObject tempReflector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        isOpen = false;
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(lineRenderer.GetPosition(1));
        if (isOpen) {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, position);
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, Mathf.Infinity)) {
                if (hit.collider.CompareTag("Reflector")) {
                    tempReflector = hit.collider.gameObject;
                    Vector3 temp = Vector3.Reflect(direction, hit.normal);
                    hit.collider.gameObject.GetComponent<LightReflector>().OpenRay(hit.point, temp);
                }
                else {
                    if (hit.collider.CompareTag("Destination")) {
                        Debug.Log("Reached destination");
                    }
                }
                lineRenderer.SetPosition(1, hit.point);
            }
            else {
                if (tempReflector) {
                    tempReflector.GetComponent<LightReflector>().CloseRay();
                    tempReflector = null;
                }
                lineRenderer.SetPosition(1, direction * 200);
            }
        }
        else {
            if (tempReflector) {
                tempReflector.GetComponent<LightReflector>().CloseRay();
                tempReflector = null;
            }
        }
    }

    public void OpenRay(Vector3 pos, Vector3 dir) {
        isOpen = true;
        position = pos;
        direction = dir;
    }

    public void CloseRay() {
        isOpen = false;
        lineRenderer.positionCount = 0;
    }

    void OnMouseDrag() {
        float rotateX = Input.GetAxis("Mouse X") * rotationSpeed;
        float rotateY = Input.GetAxis("Mouse Y") * rotationSpeed;
        // if (Input.GetKey(KeyCode.K)) {
        //     transform.RotateAround(Vector3.down, rotateX);
        // }
        // else if (Input.GetKey(KeyCode.L)) {
        //     transform.RotateAround(Vector3.forward, rotateY);
        // }
        transform.RotateAround(Vector3.down, rotateX);
        transform.RotateAround(Vector3.forward, rotateY);
    }
}
