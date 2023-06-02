using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInstance", menuName = "Rainforest/Game Instance", order = 1)]
public class GameInstance : ScriptableObject
{
    public string instanceName;

    public GameObject gameContent;

    public List<GameInstance> preRequisites;

}