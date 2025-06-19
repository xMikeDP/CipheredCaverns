using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInfo : MonoBehaviour
{
    private Dictionary<int, string> idToString = new Dictionary<int, string>();
    public GameObject panel;
    public Text text; 
    public int ID = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idToString.Add(0, "   Welcome to Ciphered Caverns! This is a small game made in Unity. Hope you have fun!");
        idToString.Add(1, "   In this room you must rotate the panels, such that the light is reflected into the intended panel.");
        idToString.Add(2, "   Here you have to use the mirror's reflection and move the cubes onto their respective color.");
        idToString.Add(3,
            "   You have to pick up the logs, 1 by 1, and put them in the campfire. Be careful, if the campfire time runs out, you lose!");
        idToString.Add(4, "   You have unlocked a new ability! You can now cast fireballs to protect yourself. Your objective is to survive.");
        idToString.Add(5, "   You have unlocked another ability! You can now Wall Jump. Use the colored walls to wall jump.");
        idToString.Add(6, "   You can now Dash! Use your new ability to reach the other side.");
        idToString.Add(7, "   This is the Boss Fight. Use the knowledge and abilities gained from the prior puzzles to defeat it!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            panel.SetActive(false);
        }
    }

    public void ShowInfo() {
        text.text = idToString[ID];
        panel.SetActive(true);
        StartCoroutine(HideInfoAfterTime());
    }

    private IEnumerator HideInfoAfterTime() {
        yield return new WaitForSeconds(8);
        panel.SetActive(false);
    }
}
