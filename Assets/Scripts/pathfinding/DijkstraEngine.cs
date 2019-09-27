using System.Collections.Generic;
using UnityEngine;

public class DijkstraEngine
{
    PathGraph graph;

    public DijkstraEngine(Tile[,] tiles, RacerEquipement equipement)
    {
        graph = RebuildPathGraph(tiles, equipement);
    }

    public PathGraph RebuildPathGraph(Tile[,] tiles, RacerEquipement equipement)
    {
        return new PathGraph(tiles, equipement);
    }

    public List<GraphNode> FindPath(Vector2 start, Vector2 target)
    {
        foreach (GraphNode node in graph.nodes)
        {
            node.pathCost = Mathf.Infinity;
        }

        GraphNode startNode = graph.GetGraphNodeFromWorldPosition(start);
        GraphNode targetNode = graph.GetGraphNodeFromWorldPosition(target);

        startNode.pathCost = 0;

        while (graph.nodes.Count > 0)
        {
            GraphNode nearest = graph.GetNearestNode();
            graph.RemoveGraphNode(nearest);

            if (nearest == targetNode){
                return ReconstructPath(nearest);
            }

            foreach (GraphEdge edge in nearest.edges)
            {
                float newPathCost = nearest.pathCost + edge.travelCost;
                if (newPathCost < edge.to.pathCost)
                {
                    edge.to.pathCost = newPathCost;
                    edge.to.previous = nearest;
                }
            }
        }
        return ReconstructPath(startNode);
    }

    public List<GraphNode> ReconstructPath(GraphNode current)
    {
        List<GraphNode> path = new List<GraphNode>();

        path.Add(current);
        while (current.previous != null)
        {
            current = current.previous;
            path.Add(current);
        }
        path.Reverse();
        return path;
    }
}
