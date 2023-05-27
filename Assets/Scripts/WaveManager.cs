using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int waveNumber = 0;
    [SerializeField] int timeBetweenWaves;
    EnemySpawnManager enemySpawnManager;
    private void Start()
    {
        
        enemySpawnManager = ReferenceManager.RM.enemySpawnManager;

        NewWave();
    }
    public void NewWave()
    {
        
        AddWave();
       // ReferenceManager.RM.uIManager.targetEnemiesInWave = enemySpawnManager.targetNumberOfEnemiesInWave;
        setWave();
        StartCoroutine(LogicBetweenWaves());
        
    }

    public void setWave()
    {
        enemySpawnManager.targetNumberOfEnemiesOnScreen += waveNumber;
        enemySpawnManager.targetNumberOfEnemiesInWave *= waveNumber;
        enemySpawnManager.remainingEnemiesinWave = enemySpawnManager.targetNumberOfEnemiesInWave;
        ReferenceManager.RM.uIManager.SetWaveBarPercentage(enemySpawnManager.remainingEnemiesinWave);
        ReferenceManager.RM.uIManager.targetEnemiesInWave = enemySpawnManager.targetNumberOfEnemiesInWave;
    }
    public void AddWave()
    {
        waveNumber++;
    }

    IEnumerator LogicBetweenWaves()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        ReferenceManager.RM.uIManager.wavePanel.SetActive(true);
        ReferenceManager.RM.clipManager.PlayClip(ReferenceManager.RM.uIManager.audioSource, ReferenceManager.RM.clipManager.fanfareThree);
        yield return new WaitForSeconds(timeBetweenWaves);
        ReferenceManager.RM.uIManager.wavePanel.SetActive(false);
        enemySpawnManager.activeWave = true;
        enemySpawnManager.betweenWaves = false;
    }
}
