using MoreMountains.Tools;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObjectPooler : MonoBehaviour
{
    [SerializeField] private List<MMSimpleObjectPooler> objectPools;
    private int numOfTypes;

    private void Awake()
    {
        numOfTypes = objectPools.Count;
    }

    private void OnDisable()
    {
        foreach (MMSimpleObjectPooler objectPool in objectPools)
        {
            objectPool.DestroyObjectPool();
        }
    }

    public GameObject GetPooledEnvObject(float seed)
    {
        int index = Mathf.Abs(Mathf.FloorToInt(seed) % numOfTypes);

        return objectPools[index].GetPooledGameObject();
    }
}
