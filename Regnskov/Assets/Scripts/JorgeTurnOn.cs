using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JorgeTurnOn : MonoBehaviour
{
    public static JorgeTurnOn Instance;

    public GameInstance gameInstance;

    public GameInstance canGivePlank;

    float timerMax = 25;

    public bool active;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
        Instance= this;

    }


    private void Awake()
    {

        if (PlayerMovement.instance.knowledge.Contains(gameInstance))
        {



            gameObject.SetActive(true);
            active = true;


        }
        else {

            active= false;
            gameObject.SetActive(false);


        } 


    }

    // Update is called once per frame
    void Update()
    {
        
        


    }
}
