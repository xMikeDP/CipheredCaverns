using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretScript : MonoBehaviour
{
    private Dictionary<int, Action> functions = new Dictionary<int, Action>();
    public int ID = 0;
    public Text collectSecretText;
    private PlayerController pc;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        functions.Add(0, LightPuzzleSecret);
        functions.Add(1, FirePuzzleSecret);
        functions.Add(2, AirPuzzleSecret);
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }

    public void ActivateSecret() {
        functions[ID].Invoke();
        pc.secretCount++;
        //StartCoroutine(DisableText());
    }

    private void LightPuzzleSecret() {
        collectSecretText.text = "You have collected the Light Puzzle Secret!";
        collectSecretText.gameObject.SetActive(true);
        //StartCoroutine(DisableText());
    }

    private void FirePuzzleSecret() {
        collectSecretText.text = "You have collected the Fire Puzzle Secret! \nYour Cast cooldown has been halved.";
        collectSecretText.gameObject.SetActive(true);
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.fireSecretObtained = true;
        
        //StartCoroutine(DisableText());
    }

    private void AirPuzzleSecret() {
        collectSecretText.text = "You have collected the Air Puzzle Secret! \nYour Dash cooldown has been halved.";
        collectSecretText.gameObject.SetActive(true);
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.airSecretObtained = true;
        
        //StartCoroutine(DisableText());
    }

    public IEnumerator DisableText() {
        yield return new WaitForSeconds(2f);
        //Debug.Log(collectSecretText.text);
        collectSecretText.gameObject.SetActive(false);
    }
}
