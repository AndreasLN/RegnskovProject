using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    Dialogue dialogueScript;
    private bool playerDetected;

    public GameObject closeTrigger;

    CloseDialogueTrigger closeDialogueTrigger;

    public DialogueCollection dialogueCollection;
    PlayerMovement pm;
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
            pm = collision.gameObject.GetComponent<PlayerMovement>();
            dialogueScript.ToggleIndicator(playerDetected);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //If we lost trigger  with the player disable playerdeteced and hide indicator
        if (collision.tag == "Player")
        {
            print("hello");
            playerDetected = false;
            pm = null;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueCollection.isActive = false;
            //dialogueScript.EndDialogue();
            DialogueController.instance.EndCollection();
        }
    }


    

    //While detected if we interact start the dialogue
    private void Update()
    {

        if (playerDetected && Input.GetMouseButtonDown(1) && closeDialogueTrigger.mousePointed && !Dialogue.instance.window.gameObject.activeSelf)
        {
            //print(dialogueScript);
            //dialogueScript.StartDialogue();
            dialogueCollection.isActive = true;
            DialogueController.instance.SetCollection(dialogueCollection, pm.knowledge, pm.posession);


        }
    }
}
