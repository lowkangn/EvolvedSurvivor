using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;

public class EnemySpawner : TimedSpawner
{
    [SerializeField] private MMSpawnAroundProperties spawnProperties;

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
        
        Health objectHealth = nextGameObject.gameObject.MMGetComponentNoAlloc<Health>();
        if (objectHealth != null)
        {
            objectHealth.Revive();
        }

        MMSpawnAround.ApplySpawnAroundProperties(nextGameObject, spawnProperties, this.transform.position);

        _lastSpawnTimestamp = Time.time;
        DetermineNextFrequency();
    }
}
