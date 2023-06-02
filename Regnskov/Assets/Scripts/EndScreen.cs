using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public bool start;

    public GameObject gameCanvas;
    public GameObject endCanvas;
    public GameObject endMenu;

    public GameObject Canvas;
    public GameObject fishControls;
    public GameObject bow;

    public float timer;
    public float blockSizeHeight;
    public float maxBlockSize;


    private void Start()
    {
        Canvas.SetActive(false);

        if (start)
        {
            gameCanvas.SetActive(false);
            endMenu.SetActive(false);
            endCanvas.SetActive(false);
            fishControls.SetActive(false);
            bow.SetActive(false);
        }
    }

    private void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, blockSizeHeight, transform.localScale.z);

    }

    private void FixedUpdate()
    {
        if (isActiveAndEnabled)
        {   

            if (blockSizeHeight < maxBlockSize)
            {
                blockSizeHeight += 0.25f;
            }
            else
            {
                Canvas.SetActive(true);
            }

        }
    }

    public void StartMiniGame()
    {
        Canvas.SetActive(false);

        gameCanvas.SetActive(true);
        fishControls.SetActive(true);
        bow.SetActive(true);
        GameObject.Destroy(gameObject);
    }


}
