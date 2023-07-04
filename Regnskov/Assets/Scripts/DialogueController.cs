using GameBaseSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public GameInstance endGame;


    public static DialogueController instance;

    public DialogueController preInstance;

    //public bool isInstance;

    public GameObject window;
    //Indicator
    public GameObject indicator;
    //Text component
    public TMP_Text dialogueText;
    //Dialogues list
    public Canvas canvas;
    public Image portrait;

    [System.Serializable]
    public class Buttons
    {
        public List<Button> buttons;
    }

    public Buttons buttons;

    DialogueTrigger trigger;

    //Writing speed
    public float writingSpeed;
    //Index on dialogue
    private int index;
    //Character index
    private int charIndex;
    //Started boolean
    private bool started;
    //Wait for next boolean
    private bool waitForNext;

    private DialogueCollection activeCollection;
    private List<GameInstance> knowledge;
    private List<GameInstance> posession;
    private DialogueChapter activeChapter;
    private DialogueChunk activeChunk;

    private bool awaitReply = false;
    private void Awake()
    {
        //Dialogue starts as closed
        ToggleIndicator(false);
        ToggleWindow(false);

        if(DialogueController.instance != null )
        {

            preInstance = DialogueController.instance;

            print(preInstance);

        }

        instance = this;


        //SetCurrentDialogue(testStartDialogue);

    }

    public void SetInstance()
    {
        instance = this;
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }
    public void SetCollection(DialogueCollection collection, List<GameInstance> knowledge, List<GameInstance> posession, DialogueTrigger dialogueTrigger = null)
    {

        if(dialogueTrigger!= null)
        {

            trigger = dialogueTrigger;

            trigger.closeDialogueTrigger.mousePointed = false;

            Cursor.SetCursor(trigger.mouseCursor, Vector2.zero, CursorMode.ForceSoftware);

            trigger.closeDialogueTrigger.gameObject.SetActive(false);
        }

        PlayerMovement.instance.stopMovement = true;
        awaitReply = false;
        activeChunk = null;
        activeChapter = null;
        activeCollection = collection;
        this.knowledge = knowledge;
        this.posession = posession;
        NextChapter();
        
    }

    public void EndCollection ()
    {
        PlayerMovement.instance.stopMovement = false;

        if(trigger !=null)
        {
            trigger.closeDialogueTrigger.gameObject.SetActive(true);

        }


        started = false;
        waitForNext = false;
        awaitReply = false;
        ToggleWindow(false);

        
       

    }

    void CheckForEnd()
    {
            
        Vector3 vector3 = new Vector3(0, 0, 0);

        End.instance.actionComponent.Activate(vector3);

    }

    public void NextChapter(DialogueChapter chapter = null)
    {
        for (int i = 0; i < buttons.buttons.Count; i++)
        {
            if (buttons.buttons[i] != null)
                buttons.buttons[i].gameObject.SetActive(false);
        }
        awaitReply = false;
        activeChunk = null;
        if (chapter == null)
        {
            activeChapter = activeCollection.NextChapter(knowledge, posession);
        }
        else
        {
            activeChapter = chapter;
        }
        if (activeChapter != null)
        {
            started = true;
            ToggleWindow(true);
            GetNextChunk();
        }
        else
        {
            EndCollection();
        }

    }

    public void GoToChapter()
    {

    }

    public void GetNextChunk(bool notClose = true)
    {
        activeChunk = activeChapter.GetNext(activeChunk);
        if (activeChunk != null)
        {

            portrait.sprite = activeChunk.face;
            dialogueText.text = "";
            charIndex = 0;
            StartCoroutine(Writing());
        }
        else if (!notClose)
        {
            EndCollection();
        }
        
    }

    IEnumerator Writing()
    {
        yield return new WaitForSecondsRealtime(writingSpeed);

        string currentDialogue = activeChunk.content;
        //Debug.Log("Active: " + currentDialogue + ", Length: " + currentDialogue.Length);
        //Write the character
        dialogueText.text += currentDialogue[charIndex];
        //increase the character index 
        charIndex++;
        //Make sure you have reached the end of the sentence
        if (charIndex < currentDialogue.Length)
        {
            //Debug.Log("Write index: " + charIndex + ", content now: " + dialogueText.text);
            //Wait x seconds 
            yield return new WaitForSecondsRealtime(writingSpeed);
            //Restart the same process
            StartCoroutine(Writing());
        }
        else
        {
            //End this sentence and wait for the next one
            waitForNext = true;
        }
    }

    private void LateUpdate()
    {
        if (!started)
            return;

        if ((waitForNext && Input.GetMouseButtonDown(0) && !awaitReply) || (waitForNext && activeChapter.replies.Count > 0 && !awaitReply && activeChapter.chunks[activeChapter.chunks.Count -1] == activeChunk))// && !canvas.isActiveAndEnabled)
        {
            
            bool notClose = (waitForNext && activeChapter.replies.Count > 0 && !awaitReply && activeChapter.chunks[activeChapter.chunks.Count - 1] == activeChunk);
            waitForNext = false;

            GetNextChunk(notClose);

            if (activeChunk != null)
            {
                //Debug.Log("Not Null");
                //charIndex = 0;
                //StartCoroutine(Writing());
            }
            else
            {
                //DialogueReply replies = activeChapter.GetReplies();
                List<DialogueReply> replies = activeChapter.replies;
                awaitReply = true;
                if (replies != null)
                {
                    int maxI = 0;
                    for (int i = 0; i < replies.Count; i++)
                    {
                        if (i < buttons.buttons.Count && buttons.buttons[i] != null)
                        {
                            buttons.buttons[i].gameObject.SetActive(true);
                            buttons.buttons[i].GetComponentInChildren<TMP_Text>().text = replies[i].replyTitle;
                            //Destroy(buttons.buttons[i].gameObject);
                        }
                        maxI = i;
                    }

                    for (int i = maxI+1; i < buttons.buttons.Count; i++)
                    {
                        if (buttons.buttons[i] != null)
                            buttons.buttons[i].gameObject.SetActive(false);
                    }
                }

                if (activeChapter.gives != null)
                {


                    if (activeChapter.gives.knowledgeObject)
                    {
                        if (!knowledge.Contains(activeChapter.gives))
                        {
                            knowledge.Add(activeChapter.gives);

                            if (knowledge.Contains(endGame))
                            {
                                CheckForEnd();
                            }


                        }
                    }
                    if (activeChapter.gives.posessionObject)
                    {
                        if (!posession.Contains(activeChapter.gives) || !activeChapter.gives.unique)
                        {
                            posession.Add(activeChapter.gives);
                        }
                    }
                }

                if (activeChapter.takes != null)
                {
                    if (activeChapter.takes.knowledgeObject)
                    {
                        if (knowledge.Contains(activeChapter.takes))
                        {
                            print("HELLO???");
                            knowledge.Remove(activeChapter.takes);
                        }
                    }
                    if (activeChapter.takes.posessionObject)
                    {
                        if (posession.Contains(activeChapter.takes) || !activeChapter.takes.unique)
                        {
                            posession.Remove(activeChapter.takes);
                        }
                    }
                }


            }
        }
    }

    public void ReplyA()
    {
        DialogueReply reply = activeChapter.replies[0];

        

        if (reply.endDialogue)
        {
            EndCollection();
            return;
        }
        NextChapter(reply.replyTarget);
    }

    public void ReplyB()
    {
        DialogueReply reply = activeChapter.replies[1];
        
        if (reply.endDialogue)
        {
            EndCollection();
            return;
        }
        NextChapter(reply.replyTarget);
    }

    public void ReplyC()
    {
        DialogueReply reply = activeChapter.replies[2];
        if (reply.endDialogue)
        {
            EndCollection();
            return;
        }
        NextChapter(reply.replyTarget);
    }
}
