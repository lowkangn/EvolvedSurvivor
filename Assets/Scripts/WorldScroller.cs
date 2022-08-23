using System.Collections.Generic;
using UnityEngine;

public class WorldScroller : MonoBehaviour
{
    [SerializeField] private List<WorldTile> tiles;
    [SerializeField] private float tileSize;
    [SerializeField] private int mapSizeInTiles;

    private WorldTile[,] tileMap;
    private Vector2 playerTilePos;
    private int mapCenter;

    private void Start()
    {
        tileMap = new WorldTile[mapSizeInTiles, mapSizeInTiles];

        foreach (WorldTile tile in tiles)
        {
            tileMap[tile.x, tile.y] = tile;
        }

        mapCenter = mapSizeInTiles / 2;
    }

    public void UpdatePlayerPosition(int x, int y)
    {
        playerTilePos = new Vector2(x, y);
        UpdateTileMap();
    }

    private void UpdateTileMap()
    {
        if (playerTilePos.x < mapCenter)
        {
            ShiftRightColumn();
        } 
        else if (playerTilePos.x > mapCenter)
        {
            ShiftLeftColumn();
        }

        if (playerTilePos.y < mapCenter)
        {
            ShiftTopRow();
        } 
        else if (playerTilePos.y > mapCenter)
        {
            ShiftBottomRow();
        }
    }

    private void ShiftBottomRow()
    {
        ShiftTiles(true, true);
    }
    private void ShiftTopRow()
    {
        ShiftTiles(true, false);
    }

    private void ShiftLeftColumn()
    {
        ShiftTiles(false, true);
    }

    private void ShiftRightColumn()
    {
        ShiftTiles(false, false);
    }

    private void ShiftTiles(bool isShiftingRow, bool isPositive)
    {
        float distanceToShift = mapSizeInTiles * tileSize;
        int mult = isPositive ? 1 : -1;
        int startPoint = isPositive ? 0 : mapSizeInTiles - 1;
        float xOffset = isShiftingRow ? 0f : distanceToShift;
        float yOffset = isShiftingRow ? distanceToShift : 0f;

        for (int i = 0; i < mapSizeInTiles; i++)
        {
            GameObject tile = isShiftingRow 
                ? tileMap[i, startPoint].gameObject 
                : tileMap[startPoint, i].gameObject;
            tile.transform.position = tile.transform.position
                + (mult * new Vector3(xOffset, yOffset, 0f));
        }

        ShiftTileMap(isShiftingRow, isPositive);
        RefreshTileIndexes();
    }

    private void ShiftTileMap(bool isShiftingRow, bool isPositive)
    {
        int mult = isPositive ? 1 : -1;
        int startPoint = isPositive ? 0 : mapSizeInTiles - 1;

        for (int i = 0; i < mapSizeInTiles - 1; i++)
        {
            for (int j = 0; j < mapSizeInTiles; j++)
            {
                int x1 = isShiftingRow ? j : startPoint + (mult * i);
                int y1 = isShiftingRow ? startPoint + (mult * i) : j;
                int x2 = isShiftingRow ? x1 : x1 + mult;
                int y2 = isShiftingRow ? y1 + mult : y1;

                WorldTile temp = tileMap[x1, y1];
                tileMap[x1, y1] = tileMap[x2, y2];
                tileMap[x2, y2] = temp;
            }
        }
    }

    private void RefreshTileIndexes()
    {
        for (int i = 0; i < mapSizeInTiles; i++)
        {
            for (int j = 0; j < mapSizeInTiles; j++)
            {
                WorldTile tile = tileMap[i, j];
                tile.x = i;
                tile.y = j;
            }
        }
    }
}
