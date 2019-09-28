using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraEngine : PathfindingEngine
{
    PathGraph graph;
    Tile[,] tiles;

    public DijkstraEngine(Tile[,] tiles, RacerEquipement equipement)
    {
        this.tiles = tiles;
        graph = RebuildPathGraph(tiles, equipement);
    }

    public PathGraph RebuildPathGraph(Tile[,] tiles, RacerEquipement equipement)
    {
        return new PathGraph(tiles, equipement);
    }

    public override IEnumerator FindPath(Vector2 start, Vector2 target, bool debug)
    {
        foreach (GraphNode node in graph.nodes)
        {
            node.pathCost = Mathf.Infinity;
        }

        GraphNode startNode = graph.GetGraphNodeFromWorldPosition(start);
        GraphNode targetNode = graph.GetGraphNodeFromWorldPosition(target);
        path = ReconstructPath(startNode);

        startNode.pathCost = 0;

        while (graph.nodes.Count > 0)
        {
            GraphNode nearest = graph.GetNearestNode();
            graph.RemoveGraphNode(nearest);

            if (nearest == targetNode)
            {
                path = ReconstructPath(nearest);
                break;
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
        yield return null;
    }

    public List<PathNode> ReconstructPath(GraphNode current)
    {
        List<PathNode> path = new List<PathNode>();

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
