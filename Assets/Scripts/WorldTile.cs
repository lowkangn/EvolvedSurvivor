using UnityEngine;

public class WorldTile : MonoBehaviour
{
    [SerializeField] public int x;
    [SerializeField] public int y;

    private WorldScroller worldScroller;

    private void Start()
    {
        worldScroller = GetComponentInParent<WorldScroller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            worldScroller.UpdatePlayerPosition(x, y);
        }    
    }
}
