using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public Vector2Int gridSize;
    public GameObject roadPrefab, mountainPrefab;

    Vector2 bottomLeft;

    public Tile[,] tileMatrix = new Tile[1, 1];

    public void DestroyAllTiles()
    {
        for (int i = transform.childCount; i > 0; i--)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    public void LinkTiles()
    {
        tileMatrix = new Tile[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                tileMatrix[x, y] = transform.GetChild(y + x * gridSize.x).GetComponent<Tile>();
            }
        }
    }

    public void CreateGrid()
    {
        DestroyAllTiles();

        bottomLeft = new Vector2(-gridSize.x / 2f + 0.5f, -gridSize.y / 2f + 0.5f);
        tileMatrix = new Tile[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2 worldPosition = bottomLeft + new Vector2(x, y);
                Tile tile = Instantiate(roadPrefab, worldPosition, Quaternion.identity, transform).GetComponent<Tile>();
                tile.Initialise(x, y);
                tileMatrix[x, y] = tile;
            }
        }
    }

    public Tile GetNodeFromWorldPostion(Vector2 worldPosition)
    {
        Vector2 gridPosition = worldPosition - bottomLeft;
        int gridX = (int)Mathf.Clamp(gridPosition.x, 0, gridSize.x - 1);
        int gridy = (int)Mathf.Clamp(gridPosition.y, 0, gridSize.y - 1);
        return tileMatrix[gridX, gridy];
    }

    public List<Tile> GetNodeNeighbors(Tile node)
    {
        List<Tile> neighbors = new List<Tile>();
        for (int x = -1; x <= 1; x++)
        {
            int gridX = node.gridX + x;
            for (int y = -1; y <= 1; x++)
            {
                if (x == 0 && y == 0) continue;

                int gridY = node.gridY + y;
                if (gridX >= 0 && gridX < gridSize.x && gridY >= 0 && gridY < gridSize.y)
                {
                    if (tileMatrix[gridX, gridY].isPassable)
                    {
                        neighbors.Add(tileMatrix[gridX, gridY]);
                    }
                }
            }
        }
        return neighbors;
    }

    public float GetDistance(Tile start, Tile target)
    {
        Vector2 distance = new Vector2(target.gridX - start.gridX, target.gridY - start.gridY);
        return distance.magnitude;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(tileMatrix[x, y].transform.position, new Vector3(.95f, .95f, 1f));
            }
        }
    }
}
