using UnityEngine;

public class MirrorMovement : MonoBehaviour {
    public Transform player;

    public Transform mirror;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        // mutam camera oglinzii in functie de pozitia playerului
        Vector3 localPlayer = mirror.InverseTransformPoint(player.position);
        transform.position = mirror.TransformPoint(new Vector3(localPlayer.x, 0 , -localPlayer.z));
        
        Vector3 lookAtMirror = mirror.TransformPoint(new Vector3(-localPlayer.x, localPlayer.y, localPlayer.z));
        transform.LookAt(lookAtMirror);
    }
}
