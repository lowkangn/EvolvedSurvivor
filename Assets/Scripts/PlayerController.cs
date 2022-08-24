using UnityEngine;
using MoreMountains;
using MoreMountains.TopDownEngine;

public class PlayerController : TopDownController2D
{
    [SerializeField] private SpawnManagerScriptableObject spawnManager;

    private void Start()
    {
        OnPlayerSpawn();
    }

    private void OnPlayerSpawn()
    {
        spawnManager.OnPlayerSpawn(this);
    }
}
