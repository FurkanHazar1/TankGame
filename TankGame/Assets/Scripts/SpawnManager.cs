using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] spawnLocations;
    [SerializeField] GameObject enemy;
    int spawLocationIndex;
    float spawnTimer = 2f;
    float spawnIncreaseTimer = 5f;


    void Start()
    {
        gameManager = gameManager.GetComponent<GameManager>();
        StartCoroutine(spawnEnemy());
        StartCoroutine(spawnEnemyTimeIncrease());
    }

    IEnumerator spawnEnemy()
    {
       
        spawLocationIndex = Random.Range(0, spawnLocations.Length);
        yield return new WaitForSeconds(spawnTimer);
        if (!gameManager._isGameOver)
        {
            Instantiate(enemy, spawnLocations[spawLocationIndex].transform.position, enemy.transform.rotation);
            StartCoroutine(spawnEnemy());
        }
    
    }
    IEnumerator spawnEnemyTimeIncrease()
    {
      
        yield return new WaitForSeconds(spawnIncreaseTimer);
        if (spawnTimer >= 0.8f)
        {
            spawnTimer -= 0.3f;
        }
        if(!gameManager._isGameOver)
            StartCoroutine(spawnEnemyTimeIncrease());
    }

}
