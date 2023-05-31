using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractDialogue : MonoBehaviour
{


    public string text;

    public int dialogueStart;

    public int dialogueEnd;


    public GameObject dialogue;

    Dialogue dialogueDialogue;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;

        dialogue = GameObject.Find("Dialogue");

        dialogueDialogue = dialogue.GetComponent<Dialogue>();

    }


    public void NextDialogue()
    {

        

        dialogueDialogue.GetDialogue(dialogueStart, dialogueEnd);

    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
