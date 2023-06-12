using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueChapter", menuName = "Rainforest/Dialogue Chapter", order = 1)]
public class DialogueChapter : ScriptableObject
{
    //public string name = "Chapter";
    public List<DialogueChunk> chunks;

    public GameInstance requiresKnowledge;

    public GameInstance requiresPosession;

    public GameInstance gives;

    public GameInstance takes;


    public GameInstance avoid;

    private DialogueChunk lastChunk;

    public bool priority;

    public List<DialogueReply> replies;

    //public DialogueReply replies;

    //public DialogueReply GetReplies()
    //{


    //return replies;
    //}
    public DialogueChunk GetNext(DialogueChunk lastChunk)
    {

        if (lastChunk != null)
        {
            int index = chunks.IndexOf(lastChunk);

            DialogueChunk nextChunk = null;

            if (index < chunks.Count - 1)
            {
                nextChunk = chunks[index + 1];
            }
            else
            {
                return null;
            }
            //lastChunk = nextChunk;

            return nextChunk;
        }
        else if (chunks.Count > 0)
        {
            //lastChunk = chunks[0];

            return chunks[0];
        }

        return null;

    }


}