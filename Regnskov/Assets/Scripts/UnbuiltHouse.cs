using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbuiltHouse : MonoBehaviour
{

    public GameInstance gameInstance;

    bool notified = false;

    public Notification notification;

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerMovement.instance.knowledge.Contains(gameInstance))
        {
            gameObject.SetActive(true);

            if (!notified)
            {
                notified= true;


                //Notification.instance.canvas.enabled= true;

                Notification.instance.NotificationTimer();


            }

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
