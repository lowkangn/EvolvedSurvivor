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

        AddAllEnemiesInWave(); 
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (hasNextWave && timePassed >= nextWaveTimestamp)
        {
            currentWave = waveList.Waves[nextWaveIndex];
            nextWaveIndex++;
            AddAllEnemiesInWave();
            SetNextTimestamp();
        }
    }

    private void AddAllEnemiesInWave()
    {
        foreach (WaveEntry entry in currentWave.waveEntries)
        {
            enemyPool.AddToPool(entry);
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
