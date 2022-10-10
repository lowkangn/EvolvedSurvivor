using MoreMountains.Tools;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NonRandomObjectPooler : MMMultipleObjectPooler
{
    public GameObject GetPooledObjectBySeed(float seed)
    {
        int index = Mathf.Abs(Mathf.FloorToInt(seed) % Pool.Count);

        return GetPooledGameObjectOfType(Pool[index].GameObjectToPool.name);
    }

    public GameObject GetPooledObjectByName(string name)
    {
        GameObject newObject = FindInactiveObject(name, _objectPool.PooledGameObjects);

        if (newObject != null)
        {
            return newObject;
        }
        else
        {
            GameObject searchedObject = FindObject(name, _objectPool.PooledGameObjects);
            GameObject newGameObject = (GameObject)Instantiate(searchedObject);

            SceneManager.MoveGameObjectToScene(newGameObject, this.gameObject.scene);
            newGameObject.transform.SetParent(_waitingPool.transform);
            newGameObject.name = name;
            _objectPool.PooledGameObjects.Add(newGameObject);

            return newGameObject;
        }
    }
}
