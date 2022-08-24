using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SpawnManager", menuName = "ScriptableObjects/SpawnManagerScriptableObject")]
public class SpawnManagerScriptableObject : ScriptableObject
{
    private UnityEvent<PlayerController> playerSpawnEvent;

    public UnityEvent<PlayerController> PlayerSpawnEvent => playerSpawnEvent;

    private void OnEnable()
    {
        playerSpawnEvent = new UnityEvent<PlayerController>();
    }

    public void OnPlayerSpawn(PlayerController player)
    {
        playerSpawnEvent.Invoke(player);
    }
}
