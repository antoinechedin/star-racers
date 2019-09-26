using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid
{
    public Node[,] nodes;
    public Vector2Int gridSize;

    public PathGrid(Tile[,] tiles, Racer racer)
    {
        gridSize = new Vector2Int(tiles.GetLength(0), tiles.GetLength(1));
        nodes = new Node[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                nodes[x, y] = new Node(x, y, tiles[x, y], racer);
            }
        }
    }

    public Node GetNodeFromWorldPostion(Vector2 worldPosition)
    {
        int x = (int)Mathf.Clamp(worldPosition.x, 0, gridSize.x - 1);
        int y = (int)Mathf.Clamp(worldPosition.y, 0, gridSize.y - 1);
        return nodes[x, y];
    }

    public List<Node> GetNodeNeighbors4(Node node)
    {
        List<Node> neighbors = new List<Node>();
        if (node.x - 1 >= 0) neighbors.Add(nodes[node.x - 1, node.y]);
        if (node.x + 1 < gridSize.x) neighbors.Add(nodes[node.x + 1, node.y]);
        if (node.y - 1 >= 0) neighbors.Add(nodes[node.x, node.y - 1]);
        if (node.y + 1 < gridSize.y) neighbors.Add(nodes[node.x, node.y + 1]);

        return neighbors;
    }

    public List<Node> GetNodeNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            int gridX = node.x + x;
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int gridY = node.y + y;
                if (gridX >= 0 && gridX < gridSize.x && gridY >= 0 && gridY < gridSize.y)
                {
                    neighbors.Add(nodes[gridX, gridY]);
                }
            }
        }
        return neighbors;
    }

    public float GetDistance(Node start, Node target)
    {
        Vector2 distance = new Vector2(target.x - start.x, target.y - start.y);
        return distance.magnitude;
    }

    /*private void OnDrawGizmos()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(nodes[x, y].worldPosition, new Vector3(.95f, .95f, 1f));
            }
        }
    }*/
}
