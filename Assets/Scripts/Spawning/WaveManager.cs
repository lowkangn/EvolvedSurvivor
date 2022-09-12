using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveListScriptableObject waveList;
    [SerializeField] private EnemyObjectPool enemyPool;

    private int nextWaveIndex = 0;
    private Wave currentWave;

    private float timePassed = 0f;
    private float nextWaveTimestamp = 0f;
    private bool hasNextWave = false;
    private int waveCount = 0;

    private void Start()
    {
        currentWave = waveList.FirstWave;
        waveCount = waveList.Waves.Count;

        if (waveCount > 0)
        {
            SetNextTimestamp();
            hasNextWave = true;
        }

        UpdateWithNextWave(); 
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (hasNextWave && timePassed >= nextWaveTimestamp)
        {
            currentWave = waveList.Waves[nextWaveIndex];
            nextWaveIndex++;
            UpdateWithNextWave();
            SetNextTimestamp();
        }
    }

    private void UpdateWithNextWave()
    {
        foreach (WaveEntry entry in currentWave.addToEnemyPool)
        {
            enemyPool.AddToPool(entry);
        }

        foreach (WaveEntry entry in currentWave.removeFromEnemyPool)
        {
            enemyPool.RemoveFromPool(entry);
        }
    }

    private void SetNextTimestamp()
    {
        if (nextWaveIndex < waveCount)
        {
            nextWaveTimestamp += waveList
                .Waves[nextWaveIndex].timeDelayInSeconds;
        } 
        else
        {
            hasNextWave = false;
        }
    }
}
