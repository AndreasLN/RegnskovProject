using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public GameObject fishPrefab;
    public BoxCollider2D SpawnArea;


    private float fishTime;
    private float spawnTime;
    private float screenX;
    private float screenY;
    private Vector2 spawnPos;
    private int spawnExtra;

    private void Start()
    {
        fishTime = 0;
        spawnTime = 3;
    }

    private void FixedUpdate()
    {
        
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

        Instantiate(fishPrefab, spawnPos, fishPrefab.transform.rotation);

        fishTime = 0;
        spawnTime = Random.Range(1, 5);
    }

}
