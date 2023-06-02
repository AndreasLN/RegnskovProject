using GameBaseSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActivateButton : MonoBehaviour
{

    Button button;

    SceneActionComponent actionComponent;

    public Vector3 characterPos;

    private void Start()
    {
        actionComponent = Resources.FindObjectsOfTypeAll<SceneActionComponent>()[0];
        button= GetComponent<Button>();


    }

    public void ButtOnChangeScene()
    {

        actionComponent.Activate(characterPos);


    }

    



}
