using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WaveEntry
{
    public GameObject enemyObject;
    public int enemyLimit;
}

[Serializable]
public struct Wave
{
    public float timeDelayInSeconds;
    public List<WaveEntry> waveEntries;
}

[CreateAssetMenu(fileName = "WaveList", menuName = "ScriptableObjects/WaveListScriptableObject")]
public class WaveListScriptableObject : ScriptableObject
{
    [SerializeField] private Wave firstWave;
    [SerializeField] private List<Wave> waves;
}
