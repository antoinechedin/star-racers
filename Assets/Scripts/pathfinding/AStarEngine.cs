﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarEngine : PathfindingEngine
{
    PathGrid grid;
    Tile[,] tiles;

    public AStarEngine(Tile[,] tiles, RacerEquipement equipement)
    {
        this.tiles = tiles;
        grid = RebuildPathGrid(tiles, equipement);
    }

    public PathGrid RebuildPathGrid(Tile[,] tiles, RacerEquipement equipement)
    {
        return new PathGrid(tiles, equipement);
    }

    public override List<PathNode> FindPath(Vector2 start, Vector2 target, bool debug)
    {
        Node startNode = grid.GetNodeFromWorldPostion(start);
        Node targetNode = grid.GetNodeFromWorldPostion(target);

        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();

        startNode.gCost = 0;
        startNode.fCost = grid.GetDistance(startNode, targetNode);
        openSet.Add(startNode);
        if (debug)
            tiles[startNode.x, startNode.y].SetDebugColor(Color.yellow);

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
                return ReconstructPath(currentNode, debug);
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            if (debug)
                tiles[currentNode.x, currentNode.y].SetDebugColor(Color.blue);

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
                        if (debug)
                            tiles[startNode.x, startNode.y].SetDebugColor(Color.yellow);
                    }
                }
            }
        }
        // Path finding as failed, return current node
        return ReconstructPath(startNode, debug);
    }

    public List<PathNode> ReconstructPath(Node current, bool debug)
    {
        List<PathNode> path = new List<PathNode>();

        path.Add(current);
        if (debug)
            tiles[current.x, current.y].SetDebugColor(Color.red);

        while (current.previousNode != null)
        {
            current = current.previousNode;
            path.Add(current);
            if (debug)
                tiles[current.x, current.y].SetDebugColor(Color.red);
        }
        path.Reverse();
        return path;
    }
}
