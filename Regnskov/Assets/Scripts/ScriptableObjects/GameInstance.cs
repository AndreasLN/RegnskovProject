using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInstance", menuName = "Rainforest/Game Instance", order = 1)]
public class GameInstance : ScriptableObject
{
    public string instanceName;
<<<<<<< Updated upstream
    public Sprite instanceSprite;


    public GameObject gameContent;
    public bool knowledgeObject = true;
    public bool posessionObject = true;
    public List<GameInstance> preRequisites;
    public bool unique = false;
=======

    public GameObject gameContent;

    public List<GameInstance> preRequisites;

>>>>>>> Stashed changes
}