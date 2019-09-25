using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    public GameGrid grid;

    public Pathfinding(GameGrid grid){
        this.grid = grid;
    }

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

                float newNeighborGCost = currentNode.gCost + (grid.GetDistance(currentNode, neighbor) * (currentNode.travelCost + neighbor.travelCost) / 2);
                if (newNeighborGCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.previousNode = currentNode;
                    neighbor.gCost = newNeighborGCost;
                    neighbor.fCost = newNeighborGCost + grid.GetDistance(neighbor, targetNode);

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
}
