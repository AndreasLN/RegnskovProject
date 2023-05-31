using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    public GameObject Canvas;

    public float timer;
    public float blockSizeHeight;
    public float maxBlockSize;


    private void Start()
    {
        
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

    public void EndMiniGame()
    {
        Debug.Log("Nu skal vi skifte scene væk fra her");
    }


}
