using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class Despawnable : MonoBehaviour
{
    [SerializeField] private MMPoolableObject poolableObject;
    [SerializeField] private float despawnDistance = 30.0f;

    private GameObject player;

    private void Start()
    {
        if (LevelManager.HasInstance)
        {
            player = LevelManager.Instance.Players[0].gameObject;
        }
    }

    private void Update()
    {
        if (Vector2.Distance(gameObject.transform.position, 
            player.transform.position) > despawnDistance)
        {
            poolableObject.Destroy();
        }
    }
}
