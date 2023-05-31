using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // vi henter UnityEngine.UI for at kunne kommunikere med knapperne, som er et UI element i unity
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private Sprite bgImage;

    public List<Button> btns = new List<Button>();


    void Start()
    {

        GetButtons();

    }

    void GetButtons()
    {

        GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");  // vi laver en array bestående af alle gameobjects med tagget PuzzleButton, som vi har assignet til alle vores buttons i unity


        for(int i = 0; i < objects.Length; i++)
        {

            btns.Add(objects[i].GetComponent<Button>());

            btns[i].image.sprite = bgImage;   // vi går ind i vores liste af buttons, videre ind i dens image og så dens sprite, og så sætter vi den til at være spriten bgImage, som vi har sat til at være baggrundsbilledet i unity

        }


    }
}
