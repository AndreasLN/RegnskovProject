using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrosshairBehavior : MonoBehaviour
{
    
    public KeyCode shoot;
    public static CrosshairBehavior instance;
    public GameObject targetPrefab;

    public bool endGame = false;
    private bool bowReady = false;
    private float bowDraw = 0;

    Vector3 tarPos;
    GameObject currentTarget;

    private void Start()
    {
        instance = this;

        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);

    }
    private void FixedUpdate()
    {
        if (Input.GetKey(shoot))
        {

            bowDraw += Time.fixedDeltaTime;

            if (bowDraw >= 1)
            {
                if (bowReady == false)
                {
                    GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
                    bowReady = true;
            
                }

            }

          
        }
        
    }

    private void Update()
    {
        
        if (endGame)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyUp(shoot))
        {
            if (bowReady)
            {

                if (currentTarget != null)
                {
                    Destroy(currentTarget);
                }

                tarPos = new Vector3(transform.position.x, transform.position.y);

                currentTarget = Instantiate(targetPrefab, tarPos, Quaternion.identity);

                bowReady = false;


            }

            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);

            bowDraw = 0;

        }


    }

}
