using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float maxHunger = 50;

    public float hunger = 50;

    public Canvas hungerCanvas;
    public Slider hungerSlider;

    public Image fade;

    float timer = 1.0f;

    public bool paused;

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
                UpdateHunger();
            }
        }
        

    }



}
