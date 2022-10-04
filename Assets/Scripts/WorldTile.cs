using MoreMountains.Tools;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    [SerializeField] public int x;
    [SerializeField] public int y;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float tileSize = 40f;

    private WorldScroller worldScroller;
    private MMMultipleObjectPooler bgObjectPool;

    private List<GameObject> bgObjects = new List<GameObject>();

    private void Awake()
    {
        worldScroller = GetComponentInParent<WorldScroller>();
        bgObjectPool = GetComponentInParent<MMMultipleObjectPooler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            worldScroller.UpdatePlayerPosition(x, y);
        }    
    }

    public void GenerateNewBackgroundObjects(float seed, float density)
    {
        foreach (GameObject bgObject in bgObjects)
        {
            bgObject.GetComponent<MMPoolableObject>().Destroy();
            bgObjects = new List<GameObject>();
        }

        int tileSizeInCells = Mathf.FloorToInt(tileSize / cellSize);
        Vector2 tilePosition = gameObject.transform.position;

        for (int i = 0; i < tileSizeInCells; i++)
        {
            for (int j = 0; j < tileSizeInCells; j++)
            {
                float generatedNoise = Mathf.PerlinNoise((tilePosition.x + i) * 0.5f + seed, (tilePosition.y + j) * 0.5f + seed);

                if (generatedNoise < density)
                {
                    GameObject bgObject = bgObjectPool.GetPooledGameObject();
                    Vector2 offset = new Vector2((i + 0.5f) * cellSize, (j + 0.5f) * cellSize);
                    bgObject.transform.position = tilePosition + offset;
                    bgObject.transform.parent = gameObject.transform;
                    bgObject.SetActive(true);

                    bgObjects.Add(bgObject);
                }
            }
        }
    }
}
