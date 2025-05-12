using System;
using UnityEngine;

public class PushObstacle : MonoBehaviour {
    
    [SerializeField] 
    private float pushForce;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body != null) {
            Vector3 pushDirection = hit.gameObject.transform.position - transform.position;
            pushDirection.y = 0;
            pushDirection.Normalize();
            
            body.AddForceAtPosition(pushDirection * pushForce, transform.position, ForceMode.Impulse);
        }
    }
}
