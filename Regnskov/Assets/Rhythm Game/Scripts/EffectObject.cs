using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{

    public float lifetime = 1;


    void Start()
    {
        
    }

    
    void Update()
    {

        Destroy(gameObject, lifetime);  // gameobject bliver destroyed ud fra den lifetime variabel 

    }
}
