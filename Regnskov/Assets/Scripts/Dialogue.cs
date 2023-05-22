using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Dialogue;
using Random = UnityEngine.Random;

public class Dialogue : MonoBehaviour
{
    //Fields
    //Window
    public GameObject window;
    //Indicator
    public GameObject indicator;
    //Text component
    public TMP_Text dialogueText;
    //Dialogues list
    public SpriteRenderer portrait;
    [System.Serializable]
    public class Buttons
    {
        public List<Button> buttons;
    }

    public Buttons buttons;


    [System.Serializable]
    public class Dialogues
    {
        public List<string> dialogues;
    }
    
    [System.Serializable]
    public class DialoguesList
    {
        public List<Dialogues> dL;
    }

    public DialoguesList dialoguesList;
    
    [System.Serializable]
    public class Faces
    {
        public List<Sprite> faces;
    }

    [System.Serializable]
    public class FacesList
    {
        public List<Faces> fL;
    }


    public FacesList facesList;

    [System.Serializable]
    public class Replies
    {
        public List<Button> replies;
    }

    [System.Serializable]
    public class RepliesList
    {
        public List<Replies> rL;
    }

    [System.Serializable]
    public class RepliesListList
    {
        public List<RepliesList> rLL;
    }


    public RepliesListList repliesListList;



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

    private int chosenDialogue;

    private void Awake()
    {
        //Dialogue starts as closed
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    //Start Dialogue
    public void StartDialogue()
    {
        //Window activates and indicator toggles out, the first dialogue in the list is taken.
        if (started)
            return;
        Time.timeScale = 0;
        //Boolean to indicate that we have started
        started = true;
        //Show the window
        ToggleWindow(true);
        //hide the indicator
        ToggleIndicator(false);
        //Finds a random dialogue
        chosenDialogue = Random.Range(0, dialoguesList.dL.Count);
        //Start with first dialogue
        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        //start index at zero
        index = i;
        //Reset the character index
        charIndex = 0;
        //clear the dialogue component text
        dialogueText.text = string.Empty;

        portrait.sprite = facesList.fL[chosenDialogue].faces[index];
        HideReplies();

        CheckReplies(0);

        //Start writing
        StartCoroutine(Writing());
    }

    public void CheckReplies(int indexReplies)
    {
        if (repliesListList.rLL[chosenDialogue].rL[index].replies[indexReplies])
        {
            print("CheckReplies");

            buttons.buttons[indexReplies].gameObject.SetActive(true);
            buttons.buttons[indexReplies] = repliesListList.rLL[chosenDialogue].rL[index].replies[indexReplies];

            CheckReplies(indexReplies + 1);

        }
        else
        {
        }
    }

    public void HideReplies()
    {

        for (int i = 0; i < buttons.buttons.Count; i++)
        {
            print(i);
            if (buttons.buttons[i])
            {
                buttons.buttons[i].enabled = false;
                print("hello");
            }
            else
            {
                print("uh oh");
            }

        }

    }

    //End Dialogue
    public void EndDialogue()
    {
        Time.timeScale = 1;
        //Stared is disabled
        started = false;
        //Disable wait for next as well
        waitForNext = false;
        //Stop all Ienumerators
        StopAllCoroutines();
        //Hide the window
        ToggleWindow(false);
    }
    //Writing logic
    IEnumerator Writing()
    {
        yield return new WaitForSecondsRealtime(writingSpeed);

        string currentDialogue = dialoguesList.dL[chosenDialogue].dialogues[index];

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

        if (waitForNext && Input.GetKeyDown(KeyCode.E))
        {
            waitForNext = false;
            index++;

            //Check if we are in the scope fo dialogues List
            if (index < dialoguesList.dL[0].dialogues.Count)
            {
                print(dialoguesList.dL);
                //If so fetch the next dialogue
                GetDialogue(index);
            }
            else
            {
                //If not end the dialogue process
                ToggleIndicator(true);
                EndDialogue();
            }
        }
    }

}
