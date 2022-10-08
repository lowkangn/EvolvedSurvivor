using System;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using MoreMountains.Feedbacks;
using TeamOne.EvolvedSurvivor;

public class EnemySpawner : TimedSpawner, MMEventListener<MMGameEvent>
{
    [SerializeField] private MMSpawnAroundProperties spawnProperties;
    [SerializeField] private SpawnManagerScriptableObject spawnManager;

    private int enemyCount;
    private float timePassed = 0f;

    protected override void Update()
    {
        base.Update();

        timePassed += Time.deltaTime;
    }

    private void OnEnable()
    {
        spawnManager.PlayerSpawnEvent.AddListener(AttachToPlayer);
        this.MMEventStartListening<MMGameEvent>();

    }

    private void OnDisable()
    {
        spawnManager.PlayerSpawnEvent.RemoveListener(AttachToPlayer);
        this.MMEventStopListening<MMGameEvent>();

        ObjectPooler.DestroyObjectPool();
    }

    public void OnMMEvent(MMGameEvent eventType)
    {
        if (eventType.EventName == "EnemyDeath")
        {
            enemyCount--;
        }
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

        nextGameObject.GetComponent<Enemy>().ScaleStats(timePassed);
        
        MMSpawnAround.ApplySpawnAroundProperties(nextGameObject, spawnProperties, this.transform.position);

        _lastSpawnTimestamp = Time.time;
        enemyCount++;

        DetermineNextFrequency();
    }

    private void AttachToPlayer(PlayerController player)
    {
        gameObject.transform.position = player.transform.position;
        gameObject.transform.parent = player.transform;
    }
}
