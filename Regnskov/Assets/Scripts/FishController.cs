using GameBaseSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public static FishController instance;


    [Header("SceneLoading")]
    public SceneActionComponent sceneLoader;
    public Vector3 characterPosition;

    [Header("Canvas Objects")]
    public List<GameObject> fishPrefabs;
    public GameObject endMenu;
    public GameObject endCanvas;
    public GameObject fishTimerText;
    public GameObject fishScoreText;
    public GameObject fishScoreNum;
    public GameObject fishTimerNum;
    public TMP_Text fishScore;
    public TMP_Text timer;
    public TMP_Text endFishScore;

    public BoxCollider2D SpawnArea;

    [Header("In Game Variables")]
    public int timerInt;
    public int fishCaught = 0;

    public float gameTimeLeft;
    public float gameTimeStart;
    public float spawnTime = 3;

    private float fishTime;
    private float screenX;
    private float screenY;
    private int spawnExtra;
    private Vector2 spawnPos;
    public GameInstance gameInstance;
    public GameInstance hasFish;


    private void Start()
    {
        instance = this;
        fishTime = 0;
        gameTimeLeft = gameTimeStart;

        
    }

    private void Update()
    {

        timerInt = (int)gameTimeLeft;

        timer.text = timerInt.ToString();

        fishScore.text = fishCaught.ToString();
        endFishScore.text = fishCaught.ToString();

    }



    private void FixedUpdate()
    {

        if (gameTimeLeft > 0)
        {
            gameTimeLeft -= Time.fixedDeltaTime;
        }
        else
        {
            
            endMenu.SetActive(true);

            fishTimerText.SetActive(false);
            fishScoreText.SetActive(false);
            fishScoreNum.SetActive(false);
            fishTimerNum.SetActive(false);

            BowController.instance.endGame = true;
            CrosshairBehavior.instance.endGame = true;

        }

        if (fishTime < spawnTime) 
        {
            fishTime += Time.fixedDeltaTime;
        }
        else
        {
            SpawnFish();

            spawnExtra = Random.Range(0, 2);

            if (spawnExtra == 1)
            {
                SpawnFish();
            }

        }

    }

    void SpawnFish()
    {
        screenX = Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.x);
        screenY = Random.Range(SpawnArea.bounds.min.y, SpawnArea.bounds.max.y);

        spawnPos = new Vector2(screenX, screenY);

        int fishPrefab = Random.Range(0, fishPrefabs.Count);

        Instantiate(fishPrefabs[fishPrefab], spawnPos, fishPrefabs[fishPrefab].transform.rotation);

        fishTime = 0;
        spawnTime = Random.Range(1, 5);
    }

    public void EndMiniGame()
    {


        if (!PlayerMovement.instance.posession.Contains(hasFish) && CustomGameManager.instance.fishPounds > 5)
        {
            PlayerMovement.instance.knowledge.Add(hasFish);

        }


        if (!PlayerMovement.instance.knowledge.Contains(gameInstance))
        {
            PlayerMovement.instance.knowledge.Add(gameInstance);

        }


        Vector3 newPos = new Vector3(characterPosition.x, characterPosition.y, characterPosition.z);

        sceneLoader.Activate(characterPosition);
    }

}
