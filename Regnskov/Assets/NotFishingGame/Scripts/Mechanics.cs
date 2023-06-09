using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanics : MonoBehaviour
{

    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;

    [SerializeField] Transform circle;

    public GameObject firePlace;

    float circlePosition;
    float circleDestination;

    float circleTimer;
    [SerializeField] float timerMultiplicator = 3f;

    float circleSpeed;
    [SerializeField] float smoothMotion = 1f;

    [SerializeField] Transform hook;
    float hookposition;

    [SerializeField] float hookSize = 0.1f;
    [SerializeField] float hookPower = 0.5f; 
    float hookProgress;
    float hookVelocity;
    [SerializeField] float hookPullPower = 0.01f;
    [SerializeField] float hookGravityPower = 0.005f;
    [SerializeField] float hookProgressDegradationPower = 0.1f;

    [SerializeField] SpriteRenderer hookSpriteRenderer;

    [SerializeField] Transform progressBarContainer;

    bool pause = false;

    [SerializeField] float failTimer = 10f;



    void Start()
    {


        Resize();

    }

    public void Resize()
    {

        Bounds b = hookSpriteRenderer.bounds;
        float xSize = b.size.x;
        Vector3 ls = hook.localScale;
        float distance = Vector3.Distance(topPivot.position, bottomPivot.position);
        ls.x = (distance / xSize * hookSize);
        hook.localScale = ls;

    }

    
    void Update()
    {

        if (pause) { return; }
        Circle();
        Hook();
        ProgressCheck();

        

    }

    private void ProgressCheck()  // funkionalitet til progressbar
    {

        Vector3 ls = progressBarContainer.localScale;
        ls.x = hookProgress;
        progressBarContainer.localScale = ls;

        float min = hookposition - hookSize / 2;
        float max = hookposition + hookSize / 2;

        if(min < circlePosition && circlePosition < max)
        {

            hookProgress += hookPower * Time.deltaTime;

            if (firePlace.GetComponent<FireplaceAnimation>().fireHigh == false ) 
            {
                firePlace.GetComponent<FireplaceAnimation>().fireHigh = true;

            }


        }
        else
        {

            firePlace.GetComponentInChildren<FireplaceAnimation>().fireHigh = false;

            hookProgress -= hookProgressDegradationPower * Time.deltaTime; // progress skal formindskes, hvis hookarea ikke er over circle

            failTimer -= Time.deltaTime;

            if(failTimer <= 0f)
            {

                Lose();

            }

        }

        if(hookProgress >= 1f)
        {

            Win();

        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);

    }

    private void Lose()
    {

        pause = true;
        Debug.Log("YOU LOSE");

    }

    private void Win()
    {

        pause = true;
        Debug.Log("YOU WIN!");


    }



    void Hook() // funktionalitet til hook
    {
        if (Input.GetMouseButton(0))
        {
            hookVelocity += hookPullPower * Time.deltaTime;
        }
        hookVelocity -= hookGravityPower * Time.deltaTime;

        hookposition += hookVelocity;


        if(hookposition - hookSize / 2 <= 0f && hookVelocity < 0f)
        {

            hookVelocity = 0f;

        }

        if(hookposition + hookSize / 2 >= 1f && hookVelocity >= 0f)
        {
            hookVelocity = 0f;
        }


        hookposition = Mathf.Clamp(hookposition, hookSize / 2, 1 - hookSize / 2);
        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookposition);
    }

    void Circle() // circle behaviour
    {

        circleTimer -= Time.deltaTime;

        if (circleTimer < 0f)
        {
            circleTimer = Random.value * timerMultiplicator;

            circleDestination = Random.value;
        }

        circlePosition = Mathf.SmoothDamp(circlePosition, circleDestination, ref circleSpeed, smoothMotion);

        circle.position = Vector3.Lerp(bottomPivot.position, topPivot.position, circlePosition);

    }
}
