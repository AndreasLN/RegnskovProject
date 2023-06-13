using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomGameManager : MonoBehaviour
{
    public static CustomGameManager instance;

    public GameInstance atBigTree;

    public Texture2D cursor;

    public Texture2D canTalk;

    public Texture2D canTalkHightlight;


    public int karma = 0;

    public float maxHunger = 50;

    public float hunger = 50;

    float updatedHunger;

    public float fishPounds;


    public Canvas hungerCanvas;
    public Slider hungerSlider;

    public Image fade;
    float timer = 20.0f;

    float jorgeMaxTimer = 60f;

    public float jorgeTimer = 60f;

    public bool paused;
    
    private void Awake()
    {
        instance = this;
        
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);

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

        if(PlayerMovement.instance.knowledge.Contains(atBigTree))
        {
            jorgeTimer -= Time.deltaTime;


            if (JorgeTurnOn.Instance != null)
            {


                if (JorgeTurnOn.Instance.active)
                {


                    if (jorgeTimer < 0)
                    {


                        if (!PlayerMovement.instance.knowledge.Contains(JorgeTurnOn.Instance.canGivePlank))
                        {
                            jorgeTimer = jorgeMaxTimer;


                            PlayerMovement.instance.knowledge.Add(JorgeTurnOn.Instance.canGivePlank);

                        }


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
