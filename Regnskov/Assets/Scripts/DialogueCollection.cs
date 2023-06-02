using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueReply
{

    public List<Button> buttons;

}
[System.Serializable]
public class DialogueChunk
{
    public string content;

    public Sprite face;


}
[System.Serializable]
public class DialogueChapter
{

    public List<DialogueChunk> chunks;

    public GameInstance requiresKnowledge;

    public GameInstance requiresPosession;

    public GameInstance gives;

    public DialogueChunk lastChunk;

    public DialogueReply replies;

    public DialogueReply GetReplies()
    {


        return replies;
    }
    public DialogueChunk GetNext()
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
            lastChunk = nextChunk;

            return lastChunk;
        }
        else if (chunks.Count > 0)
        {
            lastChunk = chunks[0];

            return lastChunk;
        }

        return null;

    }


}



public class DialogueCollection : MonoBehaviour
{
    public List<DialogueChapter> chapters;

    public bool randomizeOrder;

    public DialogueChapter NextChapter(List<GameInstance> knowledge, List<GameInstance> posession)
    {
        // c = current chapter in Iteration
        List<DialogueChapter> result = chapters.FindAll(c => (c.requiresKnowledge == null || knowledge.Contains(c.requiresKnowledge)) && (c.requiresPosession == null || posession.Contains(c.requiresPosession)) && (!knowledge.Contains(c.gives) || !c.gives.unique));

        if (result.Count > 0)
        {
            if (randomizeOrder)
            {
                int rdm = Random.Range(0, result.Count);
                return result[rdm];
            }
            return result[0];
        }

        return null;
    }



}