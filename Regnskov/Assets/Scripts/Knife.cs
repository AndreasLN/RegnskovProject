using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{

    Rigidbody2D rigidbody2d;

    Collider2D collider2d;


    // Start is called before the first frame update
    void Awake()
    {

        rigidbody2d = GetComponent<Rigidbody2D>();

        collider2d = GetComponent<Collider2D>();

        collider2d.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            collider2d.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            collider2d.enabled = false;
        }




    }

    private void FixedUpdate()
    {


        Vector2 prePos = transform.position;

        Vector2 position = rigidbody2d.position;

        float mousePosx = Input.mousePosition.x;
        float mousePosy = Input.mousePosition.y;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        position.x = mousePos.x;
        position.y = mousePos.y;

        Vector2 vector = position - prePos;

        if(prePos != position)
        {
            float angle = Vector2.Angle(prePos, position);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

        }



        rigidbody2d.MovePosition(position);


      


    }

}
