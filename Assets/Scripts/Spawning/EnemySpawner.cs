using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;

public class EnemySpawner : TimedSpawner
{
    [SerializeField] private MMSpawnAroundProperties spawnProperties;
    [SerializeField] private SpawnManagerScriptableObject spawnManager;

    private float timePassed = 0f;

    protected override void Update()
    {
        base.Update();

        timePassed += Time.deltaTime;
    }

    private void OnEnable()
    {
        spawnManager.PlayerSpawnEvent.AddListener(AttachToPlayer);
    }

    private void OnDisable()
    {
        spawnManager.PlayerSpawnEvent.RemoveListener(AttachToPlayer);
        ObjectPooler.DestroyObjectPool();
    }

    protected override void Spawn()
    {
        GameObject nextGameObject = ObjectPooler.GetPooledGameObject();
        if (nextGameObject == null) { return; }
        if (nextGameObject.GetComponent<MMPoolableObject>() == null)
        {
            throw new Exception(gameObject.name + " is trying to spawn objects that don't have a PoolableObject component.");
        }
        
        nextGameObject.SetActive(true);
        nextGameObject.MMGetComponentNoAlloc<MMPoolableObject>().TriggerOnSpawnComplete();

        nextGameObject.GetComponent<TeamOne.EvolvedSurvivor.Enemy>().ScaleStats(timePassed);
        
        MMSpawnAround.ApplySpawnAroundProperties(nextGameObject, spawnProperties, this.transform.position);

        _lastSpawnTimestamp = Time.time;
        DetermineNextFrequency();
    }

    private void AttachToPlayer(PlayerController player)
    {
        gameObject.transform.position = player.transform.position;
        gameObject.transform.parent = player.transform;
    }
}
