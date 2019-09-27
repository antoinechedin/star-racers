using System.Collections.Generic;
using UnityEngine;

public class PathGraph
{
    public List<GraphNode> nodes;
    public Vector2Int graphSize;

    public PathGraph(Tile[,] tiles, RacerEquipement equipement)
    {
        nodes = new List<GraphNode>();
        graphSize = new Vector2Int(tiles.GetLength(0), tiles.GetLength(1));
        for (int x = 0; x < graphSize.x; x++)
        {
            for (int y = 0; y < graphSize.y; y++)
            {
                nodes.Add(new GraphNode(x, y, tiles[x, y], equipement));
            }
        }

        // Link edges
        for (int x = 0; x < graphSize.x; x++)
        {
            for (int y = 0; y < graphSize.y; y++)
            {
                if (x - 1 >= 0)
                {
                    nodes[y + x * graphSize.y].edges.Add(new GraphEdge(
                        nodes[y + x * graphSize.y],
                        nodes[y + (x - 1) * graphSize.y],
                        tiles[x, y].travelCost / (2f * equipement.GetSpeedFactorFor(tiles[x, y].type)) + tiles[x - 1, y].travelCost / (2f * equipement.GetSpeedFactorFor(tiles[x - 1, y].type))
                    ));
                }
                if (x + 1 < graphSize.x)
                {
                    nodes[y + x * graphSize.y].edges.Add(new GraphEdge(
                        nodes[y + x * graphSize.y],
                        nodes[y + (x + 1) * graphSize.y],
                        tiles[x, y].travelCost / (2f * equipement.GetSpeedFactorFor(tiles[x, y].type)) + tiles[x + 1, y].travelCost / (2f * equipement.GetSpeedFactorFor(tiles[x + 1, y].type))
                    ));
                }
                if (y - 1 >= 0)
                {
                    nodes[y + x * graphSize.y].edges.Add(new GraphEdge(
                        nodes[y + x * graphSize.y],
                        nodes[y - 1 + x * graphSize.y],
                        tiles[x, y].travelCost / (2f * equipement.GetSpeedFactorFor(tiles[x, y].type)) + tiles[x, y - 1].travelCost / (2f * equipement.GetSpeedFactorFor(tiles[x, y - 1].type))
                    ));
                }
                if (y + 1 < graphSize.y)
                {
                    nodes[y + x * graphSize.y].edges.Add(new GraphEdge(
                        nodes[y + x * graphSize.y],
                        nodes[y + 1 + x * graphSize.y],
                        tiles[x, y].travelCost / (2f * equipement.GetSpeedFactorFor(tiles[x, y].type)) + tiles[x, y + 1].travelCost / (2f * equipement.GetSpeedFactorFor(tiles[x, y + 1].type))
                    ));
                }
            }
        }
    }

    public GraphNode GetGraphNodeFromWorldPosition(Vector2 worldPosition)
    {
        int x = (int)Mathf.Clamp(worldPosition.x, 0, graphSize.x - 1);
        int y = (int)Mathf.Clamp(worldPosition.y, 0, graphSize.y - 1);
        return nodes[y + x * graphSize.y];
    }

    public void RemoveGraphNode(GraphNode nodeToRemove)
    {
        foreach (GraphNode node in nodes)
        {
            List<GraphEdge> toRemove = new List<GraphEdge>();
            foreach (GraphEdge edge in node.edges)
            {
                if (edge.to == nodeToRemove)
                {
                    toRemove.Add(edge);
                }
            }
            foreach (GraphEdge toRemoveEdge in toRemove)
            {
                node.edges.Remove(toRemoveEdge);
            }
        }
        nodes.Remove(nodeToRemove);
    }

    public GraphNode GetNearestNode()
    {
        if (nodes.Count == 0) return null;
        GraphNode nearest = nodes[0];
        for (int i = 1; i < nodes.Count; i++)
        {
            if (nodes[i].pathCost < nearest.pathCost)
            {
                nearest = nodes[i];
            }
        }
        return nearest;
    }
}
