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
    public SceneActionComponent directActionComponent;
    public Vector3 characterPos;
    bool isValid = true;
    private void Start()
    {
        //actionComponent.gameObject.scene.buildIndex
        
        SceneActionComponent[] all =  Resources.FindObjectsOfTypeAll<SceneActionComponent>();
        int current = 0;
        while (all.Length > 0 && all[current].gameObject.scene.buildIndex != gameObject.scene.buildIndex)
        {
            current++;
        }

        if (current < all.Length && all.Length > 0)
        {
            actionComponent = all[current];
        }
        else
        {
            isValid = false;
        }

        button= GetComponent<Button>();


    }

    public void ButtOnChangeScene()
    {
        if (directActionComponent != null)
        {
            directActionComponent.Activate(characterPos);

        }
        else
        {
            if (isValid)
            {
                actionComponent.Activate(characterPos);
            }
           

        }


    }

    



}
