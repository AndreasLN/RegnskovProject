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

    private bool firstGuess, secondGuess;   // disse bools vil være false, da vi ikke specificere dem til at være true

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
        gameGuesses = gamePuzzles.Count / 2;

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


        if (!firstGuess)  // if true, fordi "!" = not, så altså "not false", som jo så må være true
        {

            firstGuess = true;  // denne linje sikre os, at hvis det ikke er første gang vi gætter, så vil firstGuess stadig være true

            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);  // her converter vi vores string navn fra unity til at være en integer

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];

        }
        else if(!secondGuess)
        {

            secondGuess = true;  

            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            countGuesses++;

            StartCoroutine(CheckIfThePuzzlesMatch());

        }



    }

    IEnumerator CheckIfThePuzzlesMatch()
    {

        yield return new WaitForSeconds(1f);

        if (firstGuessPuzzle == secondGuessPuzzle)
        {

            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = false;     // vi gør så at buttons ikke er interactable igen, hvis vi gætter rigtigt
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);     // vi får knapperne til at forsvinde, hvis vi gætter rigtigt
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();

        }

        else
        {

            yield return new WaitForSeconds(.5f);


            btns[firstGuessIndex].image.sprite = bgImage;     // hvis vi ikke gætter rigtigt, så skal den gemme sig igen
            btns[secondGuessIndex].image.sprite = bgImage;

        }

        yield return new WaitForSeconds(.5f);


        firstGuess = secondGuess = false;


    }


    void CheckIfTheGameIsFinished()   //   funktion der bruges til at checke om spillet er færdigt.
    {

        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("you finished the game");

            Debug.Log("it took you " + countGuesses + " many guesses to finish the game");

        }
    }
}
