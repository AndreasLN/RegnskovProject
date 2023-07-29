using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    Dialogue dialogueScript;




    public bool playerDetected;

    public GameObject closeTrigger;

    public CloseDialogueTrigger closeDialogueTrigger;

    public DialogueCollection dialogueCollection;
    PlayerMovement pm;

    public Collider2D collider2D;

    public Texture2D mouseCursor;

    Texture2D canTalk;

    Texture2D canTalkHightlight;


    private void Start()
    {

        mouseCursor = CustomGameManager.instance.cursor;

        collider2D = GetComponentInParent<Collider2D>();

        canTalk = CustomGameManager.instance.canTalk;

        canTalkHightlight = CustomGameManager.instance.canTalkHightlight;

        closeDialogueTrigger = closeTrigger.GetComponent<CloseDialogueTrigger>();
        //Debug.LogError("Count: " + Resources.FindObjectsOfTypeAll<Dialogue>().Length);
        dialogueScript = DialogueController.instance.dialogue;


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

            if (closeDialogueTrigger.mousePointed)
            {
                Cursor.SetCursor(canTalkHightlight, Vector2.zero, CursorMode.ForceSoftware);

            }
            


        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //If we lost trigger  with the player disable playerdeteced and hide indicator
        if (collision.tag == "Player")
        {
            playerDetected = false;

            pm = null;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueCollection.isActive = false;
            //dialogueScript.EndDialogue();
            DialogueController.instance.EndCollection();

            if (closeDialogueTrigger.mousePointed)
            {
                Cursor.SetCursor(canTalk, Vector2.zero, CursorMode.ForceSoftware);

            }
            else{
                Cursor.SetCursor(mouseCursor, Vector2.zero, CursorMode.ForceSoftware);

            }

        }
    }


    

    //While detected if we interact start the dialogue
    private void Update()
    {
        if (closeDialogueTrigger == null)
        {
            Debug.LogError("closeDialogueTrigger gone!!!");
            

        }
        if (DialogueController.instance == null)
        {
            Debug.LogError("DialogueController is GONE!!!");

        }
        if (closeDialogueTrigger.mousePointed && !DialogueController.instance.window.gameObject.activeSelf)
        {

            

            if (playerDetected)
            {

                Cursor.SetCursor(canTalkHightlight, Vector2.zero, CursorMode.ForceSoftware);


                if (Input.GetMouseButtonDown(1) /*!Dialogue.instance.window.gameObject.activeSelf*/)
                {
                    //print(dialogueScript);
                    //dialogueScript.StartDialogue();

                    

                    dialogueCollection.isActive = true;



                    DialogueController.instance.SetCollection(dialogueCollection, pm.knowledge, pm.posession, this);


                }
            }
            else
            {

                //Cursor.SetCursor(canTalk, Vector2.zero, CursorMode.ForceSoftware);

            }




        }

       
    }
}
