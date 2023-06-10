using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class CloseDialogueTrigger : MonoBehaviour
{

    RaycastHit hit;
    bool wRay;


    public bool mousePointed = false;

    private void OnMouseEnter()
    {
        mousePointed= true;

    }



   

    
    /*private void Update()
    {
        
        if(Input.GetMouseButtonDown(1)) {

            RaycastHit2D hit = new RaycastHit2D();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            print(hit);

            if (Physics2D.Raycast(ray.origin, ray.direction, out hit))
            {
                hit.transform.gameObject.SendMessage("HandleInput");
                print("hello");

            }
            else
            {
                print("false");
            }

        }

    }*/
    

    private void OnMouseExit()
    {
        mousePointed = false;
    }

}
