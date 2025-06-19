using UnityEngine;

public class MirrorMovement : MonoBehaviour {
    public Transform player;

    public Transform mirror;

    // Update is called once per frame
    void Update() {
        // mutam camera oglinzii in functie de pozitia playerului
        Vector3 localPlayer = mirror.InverseTransformPoint(player.position);
        transform.position = mirror.TransformPoint(new Vector3(localPlayer.x, 0 , -localPlayer.z));
        
        Vector3 lookAtMirror = mirror.TransformPoint(new Vector3(-localPlayer.x, localPlayer.y, localPlayer.z));
        transform.LookAt(lookAtMirror);
    }
}
