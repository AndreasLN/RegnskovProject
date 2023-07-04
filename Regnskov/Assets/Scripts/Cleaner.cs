using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{


    PlayerMovement player;

    CustomGameManager gameManager;

    public bool shouldPause;


    // Start is called before the first frame update
    void Start()
    {

        PlayerMovement.instance.gameObject.SetActive(false);


        CustomGameManager.instance.hungerCanvas.gameObject.SetActive(false);


        if (shouldPause)
        {
            CustomGameManager.instance.paused = true;
        }
        else
        {
            CustomGameManager.instance.paused = false;
        }


    }

}
