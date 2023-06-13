using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Notification : MonoBehaviour
{

    public static Notification instance;

    public Canvas canvas;

    float timer;

    float timerDuration = 4;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        canvas = GetComponent<Canvas>();

        //gameObject.SetActive(false);

    }

    private void Awake()
    {
        instance = this;

        canvas = GetComponent<Canvas>();


    }

    // Update is called once per frame
    void Update()
    {
        


        if (timer <= 0 && gameObject.activeSelf) {

            gameObject.SetActive(false);

        
        
        }
        else {

            timer -= Time.deltaTime;

        }

    }

    public void NotificationTimer()
    {
        timer = timerDuration;

        gameObject.SetActive(true);
        print("hello");



    }


}
