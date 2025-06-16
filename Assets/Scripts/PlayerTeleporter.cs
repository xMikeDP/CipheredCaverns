using System;
using System.Collections;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    public GameObject player;
    public GameObject destination;
    public float tpPosX, tpPosY, tpPosZ;

    private Vector3 destinationPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destinationPos = destination.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Entered Collision");
        if (other.CompareTag("Player")) {
            StartCoroutine(TeleportPlayer());
        }
    }

    private IEnumerator TeleportPlayer() {
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();
        if (pc != null) {
            pc.canMove = false;
        }
        Debug.Log("Before teleport: " + player.transform.position);
        player.transform.position = destinationPos;
        Debug.Log("After teleport: " + player.transform.position);
        Debug.Log("Teleporting this: " + player.name);
        yield return new WaitForSeconds(0.1f);
        if (pc != null) {
            pc.canMove = true;
        }
    }
}
