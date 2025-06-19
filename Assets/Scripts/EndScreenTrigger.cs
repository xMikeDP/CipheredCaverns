using System;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenTrigger : MonoBehaviour
{
    public GameObject endPanel;
    public Text endText;
    private PlayerController pc;

    void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            endText.text = "The End\n\nThanks for playing!\n\nSecrets collected: " + pc.secretCount + "/" + pc.totalSecretCount;
            endPanel.SetActive(true);
        }
    }
}
