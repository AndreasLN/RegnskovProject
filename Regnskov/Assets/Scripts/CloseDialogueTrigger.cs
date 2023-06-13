using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;
//using UnityEngine.WSA;

public class CloseDialogueTrigger : MonoBehaviour
{

    RaycastHit hit;
    bool wRay;

    public Collider2D collider;

    Texture2D mouseCursor;

    Texture2D canTalk;

    Texture2D canTalkHightlight;

    DialogueTrigger dialogueTrigger;


    private void Awake()
    {

        collider = GetComponent<Collider2D>();

        dialogueTrigger = gameObject.GetComponentInParent<DialogueTrigger>();



        mouseCursor = CustomGameManager.instance.cursor;

        canTalk = CustomGameManager.instance.canTalk;

        canTalkHightlight = CustomGameManager.instance.canTalkHightlight; 


    }

    public bool mousePointed = false;

    private void OnMouseEnter()
    {

        if (dialogueTrigger.dialogueCollection.CheckNextChapter(PlayerMovement.instance.knowledge, PlayerMovement.instance.posession, false) != null)
        {
            if (dialogueTrigger.playerDetected)
            {

                Cursor.SetCursor(canTalkHightlight, Vector2.zero, CursorMode.ForceSoftware);


            }
            else
            {
                Cursor.SetCursor(canTalk, Vector2.zero, CursorMode.ForceSoftware);

            }

            mousePointed = true;

        }



    }



   

    
    /*private void Update()
    {
        
        if(Input.GetMouseButtonDown(1)) {

            RaycastHit2D hit = new RaycastHit2D();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            print(hit);

            if (Physics2D.Raycast(ray.origin, ray.direction, out hit))
            {
                hit.transform.gameObject.SendMessage("HandleInput");
                print("hello");

            }
            else
            {
                print("false");
            }

        }

    }*/
    

    private void OnMouseExit()
    {

        Cursor.SetCursor(mouseCursor, Vector2.zero, CursorMode.ForceSoftware);

        mousePointed = false;
    }

}
