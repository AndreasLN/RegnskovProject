using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // vi henter UnityEngine.UI for at kunne kommunikere med knapperne, som er et UI element i unity
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private Sprite bgImage;

    public Sprite[] Puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>(); 

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;


    private string firstGuessPuzzle, secondGuessPuzzle;




    private void Awake()
    {

        Puzzles = Resources.LoadAll<Sprite>("Sprites/Candy");   // alle sprites i resources filen -> sprites -> candy skal loades
 
    }


    void Start()
    {

        GetButtons();
        AddListeners();
        AddGamePuzzles();

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


    void AddGamePuzzles()  // denne funktion er logikken bag hvilke billeder, der bliver valgt, og at de bliver valgt i par
    {

        int looper = btns.Count;

        int index = 0;


        for(int i = 0; i < looper; i++)
        {

            if(index == looper / 2)
            {
                index = 0;
            }

            gamePuzzles.Add(Puzzles[index]);

            index++;

        }

    }


    void AddListeners()
    {
        foreach (Button btn in btns)
        {

            btn.onClick.AddListener(() => pickAPuzzle());

        }


    }


    public void pickAPuzzle()
    {

        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;  // dette bruges til at finde navnet på det gameobject jeg klikker på.
        Debug.Log("hallo " + name);

    }
}
