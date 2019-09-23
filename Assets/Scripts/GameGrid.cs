using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid
{
    public Vector2Int gridSize;
    public Tile[,] tileMatrix;

    public GameGrid(Vector2Int gridSize)
    {
        this.gridSize = gridSize;
        tileMatrix = new Tile[gridSize.x, gridSize.y];
    }

    public Tile GetNodeFromWorldPostion(Vector2 worldPosition)
    {
        int gridX = (int)Mathf.Clamp(worldPosition.x, 0, gridSize.x - 1);
        int gridy = (int)Mathf.Clamp(worldPosition.y, 0, gridSize.y - 1);
        return tileMatrix[gridX, gridy];
    }

    public List<Tile> GetNodeNeighbors(Tile node)
    {
        List<Tile> neighbors = new List<Tile>();
        for (int x = -1; x <= 1; x++)
        {
            int gridX = node.gridX + x;
            for (int y = -1; y <= 1; y++)
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
