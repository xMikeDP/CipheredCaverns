using UnityEngine;

public class PlayerShootBeam : MonoBehaviour {
    public int maxReflections = 5;
    public Transform firePoint;
    public LineRenderer lineRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = maxReflections + 1;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ShootBeam(firePoint.position, firePoint.forward);
        }    
    }

    void ShootBeam(Vector3 startPosition, Vector3 direction) {
        Vector3 currentPosition = startPosition;
        Vector3 currentDirection = direction;
        
        lineRenderer.SetPosition(0, currentPosition); // start position
        int pointIndex = 1;

        //Debug.Log(firePoint.forward);
        for (int i = 0; i < maxReflections; i++) {
            RaycastHit hit;
            if (Physics.Raycast(currentPosition, currentDirection, out hit, Mathf.Infinity)) {
                Debug.DrawLine(currentPosition, hit.point, Color.red, 1f);
                currentPosition = hit.point;
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                pointIndex++;
            }
            else {
                lineRenderer.positionCount = pointIndex;
                break;
            }
        }
    }
}
