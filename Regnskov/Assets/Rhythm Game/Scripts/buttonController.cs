using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{

    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keyToPress;
   
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();


    }

    
    void Update()
    {

        if (Input.GetKeyDown(keyToPress))
        {

            theSR.sprite = pressedImage;  // når man trykker på keys, så skal button ændre form til pressedImage

        }

        if (Input.GetKeyUp(keyToPress))
        {

            theSR.sprite = defaultImage;  // når man løfter fingeren fra key, så skal button ændres til

        }
        
    }
}
