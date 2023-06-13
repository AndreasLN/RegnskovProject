using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbuiltHouse : MonoBehaviour
{

    public GameInstance gameInstance;

    public GameInstance notified;

    public Notification notification;

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerMovement.instance.knowledge.Contains(gameInstance))
        {
            gameObject.SetActive(true);

            if (!PlayerMovement.instance.knowledge.Contains(notified))
            {
                PlayerMovement.instance.knowledge.Add(notified);



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
