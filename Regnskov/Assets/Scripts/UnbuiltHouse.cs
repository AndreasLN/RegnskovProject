using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbuiltHouse : MonoBehaviour
{

    public GameInstance gameInstance;

    
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerMovement.instance.knowledge.Contains(gameInstance))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
