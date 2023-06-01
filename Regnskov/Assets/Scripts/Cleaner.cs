using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{


    PlayerMovement player;

    GameManager gameManager;

    public bool shouldPause;


    // Start is called before the first frame update
    void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>();

        gameManager = FindAnyObjectByType<GameManager>();

        player.gameObject.SetActive(false);

        gameManager.hungerCanvas.gameObject.SetActive(false);


        if (shouldPause)
        {
            gameManager.paused = true;
        }
        else
        {
            gameManager.paused = false;
        }


    }

}
