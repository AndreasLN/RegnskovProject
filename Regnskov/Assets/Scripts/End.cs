using GameBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{

    public static End instance;

    public SceneActionComponent actionComponent;

    private void Awake()
    {
        actionComponent= GetComponent<SceneActionComponent>();

        instance = this;
    }





}
