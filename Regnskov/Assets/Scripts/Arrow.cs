using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject splashPrefab;
    public Vector2 target;
    public float speed;
    public Vector2 dir;
    Rigidbody2D rb;
    Vector2 startPos;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.MovePosition(transform.position + (Vector3)dir * speed * Time.fixedDeltaTime);
        if ((startPos - (Vector2)transform.position).magnitude >= (target- startPos).magnitude+.5f)
        {
            Destroy(gameObject);    
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((target - (Vector2)transform.position).magnitude < .65f )
        {
            if (collision.gameObject.tag == "Fish")
            {
                FishController.instance.fishCaught += 1;
                
                if (collision.gameObject.TryGetComponent<FishBehavior>(out FishBehavior fishComp))
                {
                    fishComp.IsShot();
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        Instantiate(splashPrefab, transform.position, Quaternion.identity);
    }


}
