using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarEngine
{
    PathGrid grid;

    public AStarEngine(Tile[,] tiles, RacerEquipement equipement){
        grid = RebuildPathGrid(tiles,  equipement);
    }

    public PathGrid RebuildPathGrid(Tile[,] tiles, RacerEquipement equipement)
    {
        return new PathGrid(tiles, equipement);
    }

    public List<Node> FindPath(Vector2 start, Vector2 target)
    {
        Node startNode = grid.GetNodeFromWorldPostion(start);
        Node targetNode = grid.GetNodeFromWorldPostion(target);

        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();

        startNode.gCost = 0;
        startNode.fCost = grid.GetDistance(startNode, targetNode);
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

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

            foreach (Node neighbor in grid.GetNodeNeighbors4(currentNode))
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

    public List<Node> ReconstructPath(Node current)
    {
        List<Node> path = new List<Node>();

        path.Add(current);
        while (current.previousNode != null)
        {
            current = current.previousNode;
            path.Add(current);
        }
        path.Reverse();
        return path;
    }
}
