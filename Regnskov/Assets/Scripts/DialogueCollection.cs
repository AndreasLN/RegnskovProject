using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueReply
{
    public string replyTitle;
    public DialogueChapter replyTarget;
    public bool endDialogue = false;
    

}
[System.Serializable]
public class DialogueChunk
{
    public string content;

    public Sprite face;


}



public class DialogueCollection : MonoBehaviour
{
    DialogueChapter preResult = null;

    public bool isActive = false;
    public List<DialogueChapter> chapters;

    public bool randomizeOrder;

    public DialogueChapter NextChapter(List<GameInstance> knowledge, List<GameInstance> posession)
    {


        // c = current chapter in Iteration
        List<DialogueChapter> result = chapters.FindAll(c => (c.requiresKnowledge == null || knowledge.Contains(c.requiresKnowledge)) && (c.requiresPosession == null || posession.Contains(c.requiresPosession)) && (!knowledge.Contains(c.gives) || !c.gives.unique) && (c.avoid == null || !knowledge.Contains(c.avoid)) && c != preResult);

        result.Sort((x, y) => x.priority.CompareTo(y.priority));

        if (result.Count > 0)
        {
            if (randomizeOrder && result[result.Count - 1].priority == false)
            {
                int rdm = Random.Range(0, result.Count -1);
                preResult = result[rdm];

                return result[rdm];
                
                
            }
            else
            {

                preResult = result[result.Count - 1];

                return result[result.Count - 1];

            }



        }

        return null;
    }



}