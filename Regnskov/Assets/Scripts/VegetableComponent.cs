using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VegetableComponent : MonoBehaviour
{

    Collider2D collider2D;

    public Slider slider;

    cuttingMiniGameManager cuttingMiniGameManager;

    float height;

    bool isCutting = false;

    bool beenCut = false;

    float collisionPosy;

    void Start()
    {
        height = transform.localScale.y / 2;

        slider.value = 1;

        cuttingMiniGameManager = Resources.FindObjectsOfTypeAll<cuttingMiniGameManager>()[0];

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(collision.gameObject.transform.position.y < (gameObject.transform.position.y - height) + slider.value * height )
        {
            isCutting = true;

        }
        else
        {
            isCutting= false;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        isCutting= false;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (isCutting && !beenCut)
        {

            float sliderValue = slider.value;

            float knifePosy = collision.gameObject.transform.position.y;

            float newSliderValue;

            newSliderValue = ((gameObject.transform.position.y + height - Mathf.Clamp(knifePosy, -height, height)) / (height * 2));

            slider.value = Mathf.Clamp(newSliderValue, 0, sliderValue);

            if (slider.value <= 0.1)
            {
                cuttingMiniGameManager.CutVegetable();
                beenCut = true;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
       

    }
}
