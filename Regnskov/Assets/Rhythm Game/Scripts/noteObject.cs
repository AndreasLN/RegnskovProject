using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteObject : MonoBehaviour
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


                if(Mathf.Abs(transform.position.y) > 0.25) // mathf.Abs laver transform værdien om til et absolut tal. Så hvis værdien rammer -0.25, så bliver værdien lavet om til 0.25
                {
                    Debug.Log("hit");
                    GameManager.instance.normalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if(Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Goodhit");
                    GameManager.instance.normalGoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("perfecthit");
                    GameManager.instance.normalPerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
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

        }

    }
}
