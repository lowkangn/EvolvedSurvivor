using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyObjectPool : MMMultipleObjectPooler
{
    public void AddToPool(WaveEntry entry)
    {
        MMMultipleObjectPoolerObject searchedPooler = GetPoolObject(entry.enemyObject);

        if (searchedPooler == null)
        {
            MMMultipleObjectPoolerObject newPoolerObj = new MMMultipleObjectPoolerObject();
            newPoolerObj.GameObjectToPool = entry.enemyObject;
            newPoolerObj.PoolSize = entry.count;
            newPoolerObj.PoolCanExpand = false;
            Pool.Add(newPoolerObj);
        }
        else
        {
            searchedPooler.PoolSize += entry.count;
        }

        for (int i = 0; i < entry.count; i++)
        {
            AddOneObjectToThePool(entry.enemyObject);
        }
    }
}
