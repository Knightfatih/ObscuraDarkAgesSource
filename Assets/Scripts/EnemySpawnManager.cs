using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] float minSpawnDistance;
    [SerializeField] float maxSpawnDistance;
    [SerializeField] GameObject[] EnemyPrefab;
    [SerializeField] float verticalOffset;
    [SerializeField] float timeBetweenSpawns;
    float spawnTimer = 0;
    [field: SerializeField] public int targetNumberOfEnemiesOnScreen { get; set; }
    public float targetNumberOfEnemiesInWave;
    public float numberOfEnemiesOnScreen;
    public float remainingEnemiesinWave;
    public float enemiesSpawned;
    public bool activeWave { get; set; }
    public bool betweenWaves { get; set; }
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(numberOfEnemiesOnScreen < targetNumberOfEnemiesOnScreen && activeWave)
        {
            if (spawnTimer > timeBetweenSpawns)
            {
                Vector3 spawnPosition = new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f).normalized * Random.Range(minSpawnDistance, maxSpawnDistance);
                spawnPosition.y += verticalOffset;
                Instantiate(EnemyPrefab[0], spawnPosition, Quaternion.identity);
                numberOfEnemiesOnScreen++;
                enemiesSpawned++;
                spawnTimer = 0;
            }
        }
        if (enemiesSpawned >= targetNumberOfEnemiesInWave)
        {
            activeWave = false;
        }
        if (remainingEnemiesinWave<=0 && numberOfEnemiesOnScreen <= 0 && !betweenWaves)
        {
            betweenWaves = true;
            ReferenceManager.RM.waveManager.NewWave();
            enemiesSpawned = 0;
        }
    }
}
