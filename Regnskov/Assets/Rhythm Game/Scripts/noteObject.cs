using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteObject : GameManager
{

    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    
    void Start()
    {
        
    }

    
    void Update()
    {   

        if (Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {

                gameObject.SetActive(false);                


                // GameManager.instance.noteHit();


                if (Mathf.Abs(transform.position.y) > 0.25) // mathf.Abs laver transform værdien om til et absolut tal. Så hvis værdien rammer -0.25, så bliver værdien lavet om til 0.25
                {
                    GameManager.instance.normalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);                  
                    Destroy(gameObject);
                    
                    
                }
                else if(Mathf.Abs(transform.position.y) > 0.05f)
                {
                    GameManager.instance.normalGoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);                    
                    Destroy(gameObject);
                }
                else
                {
                    GameManager.instance.normalPerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);                    
                    Destroy(gameObject);
                    
                }

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Activator" )
        {

            canBePressed = true;

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Activator" && gameObject.activeSelf)
        {

            canBePressed = false;

            GameManager.instance.noteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);            
            Destroy(gameObject);
            

        }

    }
}
