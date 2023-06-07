using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGameManager : MonoBehaviour
{
    public static CustomGameManager instance;

    public int karma = 0;

    public float maxHunger = 50;

    public float hunger = 50;

    public float fishPounds;


    public Canvas hungerCanvas;
    public Slider hungerSlider;

    public Image fade;
    float timer = 1.0f;

    public bool paused;
    
    private void Awake()
    {
        instance = this;
        
    }

    public void UpdateHunger()
    {

        float ratio = hunger / maxHunger;

        hungerSlider.value = ratio;

    }

    private void Update()
    {

        if (!paused)
        {
            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                timer = 1.0f;

                hunger -= 1;

                hunger = Mathf.Clamp(hunger, 0, maxHunger);

                UpdateHunger();
            }
        }
        

    }



}
