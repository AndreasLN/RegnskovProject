using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{

    public Animator animator;
    public Fish fishInstance;
    private float fishTime = 0;
    private SpriteRenderer spriteR;
    private bool isShot = false;
    private bool fading = false;
    private int facing = 0;

    void Start()
    {

        facing = Random.Range(0, 2);

        spriteR = GetComponent<SpriteRenderer>();
        spriteR.color = new Color(1, 1, 1, 0);

        if (facing == 1)
        {
            spriteR.flipX = true;
        }

        StartCoroutine("FadeIn");

        fishTime = Random.Range(1, 3);

    }

    private void Update()
    {
        animator.SetBool("isDead", isShot);
    }

    public void IsShot()
    {
        isShot = true;
        
        spriteR.flipY = true;

        Aquire();

        if (fading == false)
        {
            StartCoroutine("FadeOut");
        }
        

    }

    private void FixedUpdate()
    {
        if (isShot == false)
        {
            if (fishTime > 0)
            {
                fishTime -= Time.deltaTime;
            }
            else
            {
                fishTime = 10;
                
                if (fading  == false)
                {
                    StartCoroutine("FadeOut");
                }
                

            }
        }

    }

    IEnumerator FadeIn()
    {
        for (float f = 0.05f;f <= 1; f += 0.05f) 
        {
            spriteR.color = new Color(1,1,1,f);

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOut()
    {

        fading = true;

        for (float f = 1; f >= 0; f -= 0.05f)
        {
            spriteR.color = new Color(1, 1, 1, f);

            yield return new WaitForSeconds(0.05f);
        }

        Object.Destroy(gameObject);

    }

    public void Aquire()
    {
        Food giAsFood = fishInstance as Food;

        CustomGameManager.instance.fishPounds += Random.Range(giAsFood.minPounds, giAsFood.maxPounds + 1);

        print(fishInstance);

    }

}
