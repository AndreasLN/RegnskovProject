using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSetter : MonoBehaviour
{

    GameObject cameraGameObject;

    Camera camera;

    Canvas canvas;

    BowController bowController;

    void Awake()
    {

        cameraGameObject = GameObject.FindGameObjectsWithTag("MainCamera")[0];

        camera = cameraGameObject.GetComponent<Camera>();

        if (GetComponent<Canvas>() != null )
        {
            canvas = GetComponent<Canvas>();
            canvas.worldCamera = camera;


        }


        if (GetComponent<BowController>() != null ) { 
            bowController = GetComponent<BowController>();
            bowController.mainCam = camera;


        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
