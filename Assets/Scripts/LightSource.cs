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
        // proiectam lumina in directia specificata
        if (Physics.Raycast(lightStartPoint.position, direction, out hit, Mathf.Infinity)) {
            // daca loveste un alt reflector, reflecta lumina
            if (hit.collider.CompareTag("Reflector")) {
                tempReflector = hit.collider.gameObject;
                Vector3 temp = Vector3.Reflect(direction, hit.normal);
                hit.collider.gameObject.GetComponent<LightReflector>().OpenRay(hit.point, temp);

                hit.collider.gameObject.GetComponent<Renderer>().material = hit.collider.gameObject.GetComponent<LightReflector>().reflectorON;
            }
            lineRenderer.SetPosition(1, hit.point);
        }
        else {
            // daca nu loveste niciun reflector, extindem light ray-ul 
            if (tempReflector) {
                tempReflector.GetComponent<LightReflector>().CloseRay();
                tempReflector = null;
            }
            lineRenderer.SetPosition(1, direction * 200);
        }
    }
}
