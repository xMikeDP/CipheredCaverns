using UnityEngine;
using UnityEngine.InputSystem; 

public class LightReflector : MonoBehaviour {
    float rotationSpeed = 0.2f;

    Vector3 position; // pozitia de origine a luminii
    Vector3 direction; // directia light rayului
    LineRenderer lineRenderer; // pt a vizualiza lumina

    public bool isOpen;
    GameObject tempReflector; // reflectorul pe care il loveste lumina

    public GameObject destination;

    public Material reflectorON;
    public Material reflectorOFF;
    public Material destinationON;
    public Material destinationOFF;

    void Start() {
        isOpen = false; // initial reflectorul e dezactivat
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0; // nu trasam nicio linie daca reflectorul e dezactivat
    }

    void Update() {
        if (isOpen) {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, position); // pozitia de inceput

            RaycastHit hit;
            GameObject hitReflector = null; // reflectorul lovit in fiecare frame

            if (Physics.Raycast(position, direction, out hit, Mathf.Infinity)) {
                // pt a schimba materialul
                Renderer hitRenderer = hit.collider.gameObject.GetComponent<Renderer>();

                // daca este reflector
                if (hit.collider.CompareTag("Reflector")) {
                    hitReflector = hit.collider.gameObject;
                    Vector3 reflectedDirection = Vector3.Reflect(direction, hit.normal); // directia in care se va reflecta urm light ray
                    LightReflector hitLightReflector = hitReflector.GetComponent<LightReflector>();
                    if (hitLightReflector != null) {
                        // generam light ray-ul reflectat
                        hitLightReflector.OpenRay(hit.point, reflectedDirection);
                        
                        // schimbam materialul reflectorului activat
                        if (hitRenderer != null) {
                            hitRenderer.material = reflectorON;
                        }
                    }
                }
                // daca este destinatie
                else if (hit.collider.CompareTag("Destination")) {
                    Debug.Log("Reached destination - opening door");
                    
                    // deschidem usa
                    if (destination != null && destination.GetComponent<DoorController>() != null) {
                        destination.GetComponent<DoorController>().OpenDoor();
                    }

                    // schimbam materialul
                    if (hitRenderer != null) {
                        hitRenderer.material = destinationON;
                    }
                }

                lineRenderer.SetPosition(1, hit.point); // setam pozitia in punctul unde am lovit
            }
            else {
                // daca nu lovim nimic vom extinde light ray-ul
                lineRenderer.SetPosition(1, position + direction * 200);
            }
            
            // daca nu mai loveste reflectorul pe care il lovea inainte, inchidem ray-ul de la reflectorul pe care il lovea
            if (tempReflector != null && tempReflector != hitReflector) {
                LightReflector oldLightReflector = tempReflector.GetComponent<LightReflector>();
                if (oldLightReflector != null) {
                    oldLightReflector.CloseRay();
                }
            }
    
            // updatam tempReflector-ul
            tempReflector = hitReflector;
        }
        else 
        {
            // daca reflectorul e oprit, dezactivam toate reflectoarele
            if (tempReflector != null) {
                LightReflector lightReflector = tempReflector.GetComponent<LightReflector>();
                if (lightReflector != null) {
                    lightReflector.CloseRay();
                }
                tempReflector = null;
            }

            // stergem si linia proprie
            lineRenderer.positionCount = 0;
        }
    }

    // creem un light ray din punctul pos in directia dir
    public void OpenRay(Vector3 pos, Vector3 dir) {
        isOpen = true;
        position = pos;
        direction = dir.normalized;
    }

    // stergem light ray-ul
    public void CloseRay() {
        //if (!isOpen) return;

        isOpen = false;
        lineRenderer.positionCount = 0;

        // schimbam materialul reflectorului dezactivat
        Renderer currRenderer = GetComponent<Renderer>();
        if (currRenderer != null) {
            currRenderer.material = reflectorOFF;
        }

        // va da close la celelalte reflectoare pe care le activa
        if (tempReflector != null) {
            LightReflector lightReflector = tempReflector.GetComponent<LightReflector>();
            if (lightReflector != null) {
                lightReflector.CloseRay();
            }

            tempReflector = null;
        }
    }

    // rotim reflectoarele din mouse
    void OnMouseDrag() {
        float rotateX = Input.GetAxis("Mouse X") * rotationSpeed;
        float rotateY = Input.GetAxis("Mouse Y") * rotationSpeed;
        
        if (Input.GetKey(KeyCode.K)) {
            transform.RotateAround(Vector3.down, rotateX);
        }
        else if (Input.GetKey(KeyCode.L)) {
            transform.RotateAround(Vector3.forward, rotateY);
        }
        else {
            transform.RotateAround(Vector3.down, rotateX);
            transform.RotateAround(Vector3.forward, rotateY);
        }
    }
}