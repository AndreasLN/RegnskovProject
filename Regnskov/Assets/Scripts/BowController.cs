using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{

    public static BowController instance;

    public KeyCode shoot;

    public Arrow arrowPrefab;

    public Animator animator;

    public float arrowSpeed;
    public float arrowError;

    public bool endGame = false;
    private bool bowReady = false;
    private bool shootFish = false;
    private bool bowDrawing = false;
    private bool bowIdle = true;
    private float bowDraw = 0;

    public Camera mainCam;

    private Vector3 mousePos;
    private void Start()
    {
        instance = this;
        bowIdle = true;
    }

    private void FixedUpdate()
    {

        if (Input.GetKey(shoot))
        {

            shootFish = false;

            bowIdle = false;

            bowDraw += Time.fixedDeltaTime;

            bowDrawing = true;

            if (bowDraw >= 1)
            {
                if (bowReady == false)
                {
                    bowDrawing = false;
                    bowReady = true;

                }

            } 

        }

    }

    private void Update()
    {
        if (endGame)
        {
            Destroy(gameObject);
        }

        animator.SetBool("bowDraw", bowDrawing);
        animator.SetBool("bowReady", bowReady);
        animator.SetBool("shootFish", shootFish);
        animator.SetBool("bowIdle", bowIdle);

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,rotZ+270);

        if (Input.GetKeyUp(shoot))
        {

            bowDrawing = false;

            if (bowReady)
            {
                shootFish = true;

                Vector3 targetPos = CrosshairBehavior.instance.transform.position;   
                    
                Arrow arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
                arrow.target = targetPos;
                arrow.dir = ((targetPos - transform.position) + (Vector3)Random.insideUnitCircle * arrowError).normalized;
                arrow.speed = arrowSpeed;
                
                bowReady = false;
                    
            }

            bowIdle = true;
            bowDraw = 0;
        }


    }

}
