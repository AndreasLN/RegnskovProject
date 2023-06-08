using GameBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public SceneActionComponent sceneLoader;

    public Vector3 characterPosition;
    public List<GameInstance> requiresKnowledge;
    public List<GameInstance> requiresPosession;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("hello");
        }
        else
        {
            print("bruh");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();

            bool allowEntrance = true;

            for (int i = 0; i < requiresKnowledge.Count; i++)
            {
                if (!pm.knowledge.Contains(requiresKnowledge[i]))
                {
                    allowEntrance = false;
                    break;
                }
            }

            for (int i = 0; i < requiresPosession.Count && allowEntrance; i++)
            {
                if (!pm.posession.Contains(requiresPosession[i]))
                {
                    allowEntrance = false;
                    break;
                }
            }

            if (allowEntrance)
            {
                GameObject player = collision.gameObject;

                Vector3 newPos = new Vector3(characterPosition.x, characterPosition.y, characterPosition.z);

                //collision.gameObject.transform.position = new Vector3(characterPosition.x, characterPosition.y, characterPosition.z);


                sceneLoader.Activate(characterPosition);
            }

            




        }
        else
        {
            print("bruh");
        }

    }
}
