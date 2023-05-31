using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtonScript : MonoBehaviour
{

    [SerializeField] private Transform puzzleField;

    [SerializeField] private GameObject btn;

    private void Awake()
    {

        for(int i = 0; i < 8; i++)
        {
            GameObject button = Instantiate(btn);  // gameobjectet btn bliver instantieret, og bliver lagt ind i et nyt gameobject vi kalder for button

            button.name = "" + i;    // her differentierer vi mellem buttons ved at give dem et forskelligt nummer som navn

            button.transform.SetParent(puzzleField, false);  // buttons parent bliver sat til puzzlefield, og worldposition bliver sat til false



        }

    }
}
