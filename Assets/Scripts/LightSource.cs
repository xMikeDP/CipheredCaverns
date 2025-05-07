using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] Transform lightStartPoint;

    private Vector3 direction;
    LineRenderer lineRenderer;
    GameObject tempReflector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        direction = lightStartPoint.forward;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, lightStartPoint.position);
    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(lightStartPoint.position, direction, out hit, Mathf.Infinity)) {
            if (hit.collider.CompareTag("Reflector")) {
                tempReflector = hit.collider.gameObject;
                Vector3 temp = Vector3.Reflect(direction, hit.normal);
                hit.collider.gameObject.GetComponent<LightReflector>().OpenRay(hit.point, temp);
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
}
