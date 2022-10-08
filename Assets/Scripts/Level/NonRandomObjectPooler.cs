using MoreMountains.Tools;
using System;
using UnityEngine;

public class NonRandomObjectPooler : MMMultipleObjectPooler
{
    public GameObject GetPooledObjectBySeed(float seed)
    {
        int index = Mathf.Abs(Mathf.FloorToInt(seed) % Pool.Count);

        return GetPooledGameObjectOfType(Pool[index].GameObjectToPool.name);
    }

    public GameObject GetPooledObjectByName(string name)
    {
        return GetPooledGameObjectOfType(name);
    }
}
