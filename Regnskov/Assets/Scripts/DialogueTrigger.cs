using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript;
    private bool playerDetected;

    public GameObject closeTrigger;

    CloseDialogueTrigger closeDialogueTrigger;

    private void Awake()
    {
        closeDialogueTrigger = closeTrigger.GetComponent<CloseDialogueTrigger>();

        dialogueScript = Resources.FindObjectsOfTypeAll<Dialogue>()[0];

    }

    //Detect trigger with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If we triggerd the player enable playerdeteced and show indicator
        if (collision.tag == "Player")
        {
            playerDetected = true;
            dialogueScript.ToggleIndicator(playerDetected);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //If we lost trigger  with the player disable playerdeteced and hide indicator
        if (collision.tag == "Player")
        {
            playerDetected = false;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueScript.EndDialogue();
        }
    }
    //While detected if we interact start the dialogue
    private void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E) && closeDialogueTrigger.mousePointed)
        {
            print(dialogueScript);
            dialogueScript.StartDialogue();
        }
    }
}