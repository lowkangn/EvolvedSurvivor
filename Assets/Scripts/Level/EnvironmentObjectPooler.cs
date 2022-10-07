using MoreMountains.Tools;
using UnityEngine;

public class EnvironmentObjectPooler : MMMultipleObjectPooler
{
    public GameObject GetPooledEnvObject(float seed)
    {
        int index = Mathf.Abs(Mathf.FloorToInt(seed) % Pool.Count);

        return GetPooledGameObjectOfType(Pool[index].GameObjectToPool.name);
    }
}
