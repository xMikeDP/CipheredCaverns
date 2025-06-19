using System;
using UnityEngine;

public class PushObstacle : MonoBehaviour 
{
    public float pushForce = 1;

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
