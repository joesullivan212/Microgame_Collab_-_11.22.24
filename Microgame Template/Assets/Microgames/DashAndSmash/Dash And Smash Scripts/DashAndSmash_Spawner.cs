using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAndSmash_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform player;
    [SerializeField] private float SpawnTimeReduceAmount;
    [SerializeField] private float SpawnTimeMin;

    void Start()
    {
        StartCoroutine(SpawnEnemies());

        SpawnEnemy();
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
            ReduceSpawnTime();
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<DashAndSmash_Enemy>().Initialize(player);
        
    }

    void ReduceSpawnTime() 
    {
        spawnInterval -= SpawnTimeReduceAmount;

        if(spawnInterval <= SpawnTimeMin) 
        { 
          spawnInterval = SpawnTimeMin;
        }
    }
}