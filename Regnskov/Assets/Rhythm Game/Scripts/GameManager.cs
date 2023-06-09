using GameBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public CustomGameManager gameManager;

    [Header("SceneLoading")]
    public SceneActionComponent sceneLoader;
    public Vector3 characterPosition;


    [Header("RythemGameVar")]
    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public Text scoretext;
    public Text multiText;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public float startNotes;
    public float notesNow;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    private bool canEnd;
    public GameInstance gameInstance;


    void Start()
    {
        instance = this;

        scoretext.text = "score: 0";

        currentMultiplier = 1;

        startNotes = FindObjectsOfType<noteObject>().Length; // regner ud hvor mange pile, der er i spillet. Pilene har noteObject script tilknyttet, og så finder vi bare længden af objects med den type på sig 
    }

    
    void Update()
    {
        
        if (canEnd) // når man kan slutte spillet skal man trykke på escape for at komme ud af det.
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Vector3 newPos = new Vector3(characterPosition.x, characterPosition.y, characterPosition.z);

                sceneLoader.Activate(characterPosition);
            }
        }
        notesNow = FindObjectsOfType<noteObject>().Length; // denne variabel skal sige hvor mange pile, der er lige nu

        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
                
            }
          
        }
        else
        {

            if(!theMusic.isPlaying && !resultScreen.activeInHierarchy)    // hvis musikken ikke spiller og resultscreen ikke er aktiv, så skal resultscreen være aktiv
            {

                CustomGameManager.instance.karma += currentScore;

                resultScreen.SetActive(true);
                canEnd = true; //tilføjet den her bool for at lettere bare kunne komme ud af spillet efter en button af en eller anden grund ikke virkede

                normalsText.text = "" + normalHits;
                goodsText.text = "" + goodHits;
                perfectsText.text = "" + perfectHits;
                missesText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / startNotes) * 100f;          // her udregner vi procenten af notes, der er ramt ud fra totalNotes

                 // percentHitText.text = percentHit.ToString("F1") + "%";   // "F1" kan bruges for kun at vise en enkelt decimal

                string rankValue = "F";

                if(percentHit > 40)
                {
                    rankValue = "D";

                    if(percentHit > 55)
                    {
                        rankValue = "C";

                            if (percentHit > 70)
                        {
                            rankValue = "B";

                            if (percentHit > 85)
                            {
                                rankValue = "A";

                                if (percentHit > 95)
                                {
                                    rankValue = "S";
                    }
                            }
                        }
                    }
                }

              
                rankText.text = rankValue;

                finalScoreText.text = currentScore.ToString();
            

            }

        }

        if (notesNow <= 0)
        {

            theMusic.Stop();

        }
    }

    public void EndMinigame()
    {

        if (!PlayerMovement.instance.knowledge.Contains(gameInstance))
        {
            PlayerMovement.instance.knowledge.Add(gameInstance);

        }


        Vector3 newPos = new Vector3(characterPosition.x, characterPosition.y, characterPosition.z);

        sceneLoader.Activate(characterPosition);

    }

    public void noteHit()
    {
        Debug.Log("hit on time");

        if(currentMultiplier - 1 < multiplierThresholds.Length) { // dette if statement gør at vi ikke overstiger Threshold værdierne, så vi ikke får nogle array index errors

            multiplierTracker++;

        if(multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
        {
            multiplierTracker = 0;
            currentMultiplier++;
        }

        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        // currentScore += scorePerNote * currentMultiplier;
        scoretext.text = "Score: " + currentScore;
    }

    public void normalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        noteHit();

        normalHits++;
    }

    public void normalGoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        noteHit();

        goodHits++;
    }

    public void normalPerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        noteHit();

        perfectHits++;
    }

    public void noteMissed()
    {
        Debug.Log("missed note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;

        missedHits++;
    }


}
