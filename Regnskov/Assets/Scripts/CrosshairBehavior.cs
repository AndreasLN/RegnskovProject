using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrosshairBehavior : MonoBehaviour
{
    public KeyCode shoot;
    public TMP_Text fishScore;


    private bool fishAming = false;
    private bool bowReady = false;
    private float bowDraw = 0;
    private float fishesCaught;



    private void FixedUpdate()
    {
        if (Input.GetKey(shoot))
        {
           
            bowDraw += Time.fixedDeltaTime;

            if (bowDraw >= 1)
            {
                if (bowReady == false)
                {
            
                    bowReady = true;
                    Debug.Log("you have my bow");
            
                }

            }

          
        }
        
    }

    private void Update()
    {
        
        fishScore.text = fishesCaught.ToString();
        
        if (Input.GetKeyUp(shoot))
        {
            if (bowReady)
            {
                if (fishAming)
                {

                    fishesCaught += 1;
                    Debug.Log("BANG");
                    bowReady = false;

                }
                else
                {

                    Debug.Log("Sploosh");
                    bowReady = false;

                }


            }

            bowDraw = 0;

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
           fishAming = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (fishAming)
        {
            fishAming = false;
        }
    }

}
