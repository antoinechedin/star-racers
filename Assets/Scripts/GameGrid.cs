using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public Vector2Int gridSize;

    Vector2 bottomLeft;
    Node[,] nodeMatrix;

    private void OnValidate()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        bottomLeft = new Vector2(-gridSize.x / 2f + 0.5f, -gridSize.y / 2f + 0.5f);
        nodeMatrix = new Node[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                nodeMatrix[x, y] = new Node(x, y, bottomLeft + new Vector2(x, y));
            }
        }
    }

    public Node GetNodeFromWorldPostion(Vector2 worldPosition)
    {
        Vector2 gridPosition = worldPosition - bottomLeft;
        int gridX = (int)Mathf.Clamp(gridPosition.x, 0, gridSize.x - 1);
        int gridy = (int)Mathf.Clamp(gridPosition.y, 0, gridSize.y - 1);
        return nodeMatrix[gridX, gridy];
    }

    public List<Node> GetNodeNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            int gridX = node.gridX + x;
            for (int y = -1; y <= 1; x++)
            { 
                if (x == 0 && y == 0) continue;

                int gridY = node.gridY + y;
                if (gridX >= 0 && gridX < gridSize.x && gridY >= 0 && gridY < gridSize.y){
                    neighbors.Add(nodeMatrix[gridX, gridY]);
                }
            }
        }
        return neighbors;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Gizmos.color = Color.white;
                if (nodeMatrix[x, y].selected) Gizmos.color = Color.red;
                Gizmos.DrawWireCube(nodeMatrix[x, y].worldPosition, new Vector3(.95f, .95f, 0f));
            }
        }
    }
}
