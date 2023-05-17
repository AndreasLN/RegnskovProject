using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    
    private float fishTime = 0;
    private SpriteRenderer spriteR;
 
    
    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        spriteR.color = new Color(1, 1, 1, 0);
        
        StartCoroutine("FadeIn");

        fishTime = Random.Range(1, 3);

    }

    private void FixedUpdate()
    {
        if (fishTime > 0) 
        {
            fishTime -= Time.deltaTime;
        }
        else
        {
            fishTime = 10;
            StartCoroutine("FadeOut");

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
        for (float f = 1; f >= 0; f -= 0.05f)
        {
            spriteR.color = new Color(1, 1, 1, f);

            yield return new WaitForSeconds(0.05f);
        }

        Object.Destroy(gameObject);

    }


}
