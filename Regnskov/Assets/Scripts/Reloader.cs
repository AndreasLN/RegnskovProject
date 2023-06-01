using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloader : MonoBehaviour
{
    PlayerMovement player;

    GameManager gameManager;

    public bool shouldPause;

    public Collider2D cameracollider;

    CinemachineConfiner2D confiner;

    // Start is called before the first frame update
    void Awake()
    {
        player = Resources.FindObjectsOfTypeAll<PlayerMovement>()[0];
        print(player);
        player.gameObject.SetActive(true);

        gameManager = Resources.FindObjectsOfTypeAll<GameManager>()[0];

        gameManager.hungerCanvas.gameObject.SetActive(true);

        if (shouldPause)
        {
            gameManager.paused = true;
        }
        else
        {
            gameManager.paused = false;
        }

        confiner = Resources.FindObjectsOfTypeAll<CinemachineConfiner2D>()[0];

        if (confiner)
        {
            confiner.m_BoundingShape2D = cameracollider;

        }


    }
}
