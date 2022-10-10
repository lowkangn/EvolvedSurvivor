using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SpawnManager", menuName = "ScriptableObjects/SpawnManagerScriptableObject")]
public class SpawnManagerScriptableObject : ScriptableObject
{
    private UnityEvent<PlayerController> playerSpawnEvent;
    private UnityEvent<GameObject> enemyDespawnEvent;

    public UnityEvent<PlayerController> PlayerSpawnEvent => playerSpawnEvent;
    public UnityEvent<GameObject> EnemyDespawnEvent => enemyDespawnEvent;

    private void OnEnable()
    {
        playerSpawnEvent = new UnityEvent<PlayerController>();
        enemyDespawnEvent = new UnityEvent<GameObject>();
    }

    public void OnPlayerSpawn(PlayerController player)
    {
        playerSpawnEvent.Invoke(player);
    }

    public void OnEnemyDespawn(GameObject enemy)
    {
        enemyDespawnEvent.Invoke(enemy);
    }
}
