using MoreMountains.Tools;
using System.Collections.Generic;
using TeamOne.EvolvedSurvivor;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    [SerializeField] public int x;
    [SerializeField] public int y;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float tileSize = 40f;

    private WorldScroller worldScroller;
    private NonRandomObjectPooler bgObjectPool;
    private MMMultipleObjectPooler easterEggPool;

    private float chanceForEasterEgg = 0.5f;

    private List<GameObject> bgObjects = new List<GameObject>();

    private void Awake()
    {
        worldScroller = GetComponentInParent<WorldScroller>();
        bgObjectPool = GetComponentInParent<NonRandomObjectPooler>();
        easterEggPool = GetComponentInParent<MMMultipleObjectPooler>();
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
        Random.InitState(Mathf.FloorToInt(seed + transform.position.x + transform.position.y));

        bool shouldSpawnEasterEgg = Random.Range(0f, 1f) < chanceForEasterEgg;
        bool hasEasterEggSpawned = false;

        Random.InitState(Mathf.FloorToInt(seed));

        for (int i = 0; i < tileSizeInCells; i++)
        {
            for (int j = 0; j < tileSizeInCells; j++)
            {
                Vector2 tileOffset = new Vector2((i + 0.5f) * cellSize, (j + 0.5f) * cellSize);
                Vector2 objectRoughPosition = tilePosition + tileOffset;

                // Do not spawn around the player's spawn
                if (objectRoughPosition.x > -5f && objectRoughPosition.x < 8f && objectRoughPosition.y > -8f && objectRoughPosition.y < 8f)
                {
                    continue;
                }

                float generatedNoise = Mathf.PerlinNoise((tilePosition.x + i) * 0.5f + seed, (tilePosition.y + j) * 0.5f + seed);

                // Spawn object if below threshold
                if (generatedNoise >= density)
                {
                    continue;
                }

                GameObject objectToSpawn;

                // Pick either easter egg or background object to spawn
                if (shouldSpawnEasterEgg && !hasEasterEggSpawned)
                {
                    objectToSpawn = easterEggPool.GetPooledGameObject();
                    hasEasterEggSpawned = true;
                }
                else
                {
                    objectToSpawn = bgObjectPool.GetPooledObjectBySeed(seed + i + j);
                }

                Vector2 randomOffset = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
                objectToSpawn.transform.position = tilePosition + tileOffset + randomOffset;
                objectToSpawn.transform.parent = gameObject.transform;
                objectToSpawn.transform.rotation = (Random.Range(0f, 1f) <= 0.5f)
                    ? Quaternion.Euler(0f, 0f, 0f)
                    : Quaternion.Euler(0f, 180f, 0f);

                objectToSpawn.SetActive(true);
                bgObjects.Add(objectToSpawn);
            }
        }
    }
}
