using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GameGrid grid;
    public bool debug;

    public List<Tile> ReconstructPath(Tile current)
    {
        List<Tile> path = new List<Tile>();

        path.Add(current);
        while (current.previousNode != null)
        {
            current = current.previousNode;
            path.Add(current);
        }
        path.Reverse();
        return path;
    }

    public List<Tile> FindPath(Vector2 start, Vector2 target)
    {
        Tile startNode = grid.GetNodeFromWorldPostion(start);
        Tile targetNode = grid.GetNodeFromWorldPostion(target);

        List<Tile> openSet = new List<Tile>();
        List<Tile> closedSet = new List<Tile>();

        startNode.gCost = 0;
        startNode.fCost = grid.GetDistance(startNode, targetNode);
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Tile currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (currentNode.fCost > openSet[i].fCost)
                {
                    currentNode = openSet[i];
                }
            }

            if (currentNode == targetNode)
            {
                return ReconstructPath(currentNode);
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            foreach (Tile neighbor in grid.GetNodeNeighbors(currentNode))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float newNeighborGCost = currentNode.gCost + grid.GetDistance(currentNode, neighbor);
                if (newNeighborGCost < neighbor.gCost || neighbor.previousNode == null)
                {
                    neighbor.previousNode = currentNode;
                    neighbor.gCost = newNeighborGCost;
                    neighbor.fCost = newNeighborGCost + grid.GetDistance(currentNode, neighbor);

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        // Path finding as failed, return current node
        return ReconstructPath(startNode);
    }

    private void Start()
    {

        Tile start = grid.tileMatrix[0, 0];
        Tile target = grid.tileMatrix[grid.gridSize.x - 1, grid.gridSize.y - 1];

        List<Tile> path = FindPath(start.transform.position, target.transform.position);

        foreach (Tile tile in path)
        {
            tile.GetComponent<SpriteRenderer>().color = Color.red;
        }        
    }
}
