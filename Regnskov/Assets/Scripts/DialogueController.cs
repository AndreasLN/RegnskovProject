using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController instance;
    public GameObject window;
    //Indicator
    public GameObject indicator;
    //Text component
    public TMP_Text dialogueText;
    //Dialogues list
    public Canvas canvas;
    public SpriteRenderer portrait;

    [System.Serializable]
    public class Buttons
    {
        public List<Button> buttons;
    }

    public Buttons buttons;


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
    private void Awake()
    {
        //Dialogue starts as closed
        ToggleIndicator(false);
        ToggleWindow(false);
        instance = this;
        //SetCurrentDialogue(testStartDialogue);

    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }
    public void SetCollection(DialogueCollection collection, List<GameInstance> knowledge, List<GameInstance> posession)
    {
        activeCollection = collection;
        this.knowledge = knowledge;
        this.posession = posession;
        NextChapter();
        started = true;
    }

    public void NextChapter()
    {

        activeChapter = activeCollection.NextChapter(knowledge, posession);

        if (activeChapter != null)
        {
            GetNextChunk();
        }

    }

    public void GoToChapter()
    {

    }

    public void GetNextChunk()
    {

        activeChunk = activeChapter.GetNext();
        if (activeChunk != null)
        portrait.sprite = activeChunk.face;
    }

    IEnumerator Writing()
    {
        yield return new WaitForSecondsRealtime(writingSpeed);

        string currentDialogue = activeChunk.content;

        //Write the character
        dialogueText.text += currentDialogue[charIndex];
        //increase the character index 
        charIndex++;
        //Make sure you have reached the end of the sentence
        if (charIndex < currentDialogue.Length)
        {
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

    private void Update()
    {
        if (!started)
            return;

        if (waitForNext && Input.GetKeyDown(KeyCode.E) && !canvas.isActiveAndEnabled)
        {
            waitForNext = false;

            GetNextChunk();

            if (activeChunk != null)
            {
                StartCoroutine(Writing());
            }
            else
            {
                DialogueReply replies = activeChapter.GetReplies();
                if (replies != null)
                {
                    for (int i = 0; i < buttons.buttons.Count; i++)
                    {
                        if (buttons.buttons[i] != null)
                        {
                            Destroy(buttons.buttons[i].gameObject);
                        }
                    }
                    for (int i = 0; i < replies.buttons.Count; i++)
                    {
                        if (i < buttons.buttons.Count)
                        {
                            buttons.buttons[i] = Instantiate(replies.buttons[i]);

                        }
                    }
                }
            }
        }
    }

}
