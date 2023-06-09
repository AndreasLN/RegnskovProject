using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public bool stopMovement = false;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public CustomGameManager gameManager;

    public Animator animator;

    public float speed = 5f;

    public float speedMax = 5f;
    public List<GameInstance> knowledge;
    public List<GameInstance> posession;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        rigidbody2d = GetComponent<Rigidbody2D>();
        gameManager = CustomGameManager.instance;

    }

    private void Awake()
    {
        
        DontDestroyOnLoad(this);

    }

    void SetSpeed()
    {

        if (gameManager.hunger > gameManager.maxHunger * 0.75f)
        {
            speed = speedMax;
        }



        if (gameManager.hunger <= gameManager.maxHunger * 0.75f)
        {

            if (gameManager.hunger <= gameManager.maxHunger * 0.5f)
            {


                if (gameManager.hunger <= gameManager.maxHunger * 0.25f)
                {


                    if (gameManager.hunger <= 0)
                    {

                        speed = speedMax * 0.4f;

                    }
                    else
                    {
                        speed = speedMax * 0.67f;
                    }

                }
                else
                {
                    speed = speedMax * 0.75f;
                }

            }
            else
            {
                speed = speedMax * 0.9f;
            }



        }


    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (!stopMovement)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
           
        }


        if(stopMovement)
        {
            horizontal = 0;
            vertical = 0;
            speed = 0.0f;
        }
        else
        {

            SetSpeed();

        }



    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }
}
