using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSetter : MonoBehaviour
{

    GameObject cameraGameObject;

    Camera camera;

    Canvas canvas;

    void Awake()
    {

        cameraGameObject = GameObject.FindGameObjectsWithTag("MainCamera")[0];

        camera = cameraGameObject.GetComponent<Camera>();

        canvas = GetComponent<Canvas>();

        canvas.worldCamera = camera;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
