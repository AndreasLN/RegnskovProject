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

    float updatedHunger;

    public float fishPounds;


    public Canvas hungerCanvas;
    public Slider hungerSlider;

    public Image fade;
    float timer = 20.0f;

    float jorgeMaxTimer = 25f;

    float jorgeTimer;

    public bool paused;
    
    private void Awake()
    {
        instance = this;
        
    }

    public void UpdateHunger()
    {

        float ratio = hunger / maxHunger;

        hungerSlider.value = ratio;

        updatedHunger = hunger;

    }

    private void Update()
    {


        if(hunger != updatedHunger)
        {
            UpdateHunger();
        }

        if(JorgeTurnOn.Instance != null)
        {
            if (JorgeTurnOn.Instance.active)
            {
                jorgeTimer -= Time.deltaTime;

                if (timer < 0)
                {
                    if (!PlayerMovement.instance.knowledge.Contains(JorgeTurnOn.Instance.canGivePlank))
                    {
                        jorgeTimer = jorgeMaxTimer;


                        PlayerMovement.instance.knowledge.Add(JorgeTurnOn.Instance.canGivePlank);

                    }


                }
            }
        }

        

        if (!paused)
        {
            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                timer = 20.0f;

                hunger -= 1;

                hunger = Mathf.Clamp(hunger, 0, maxHunger);

                UpdateHunger();
            }
        }
        

    }



}
