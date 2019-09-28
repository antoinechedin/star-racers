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
        path = ReconstructPath(startNode, debug);

        startNode.pathCost = 0;

        while (graph.nodes.Count > 0)
        {
            GraphNode nearest = graph.GetNearestNode();
            graph.RemoveGraphNode(nearest);
            if (debug)
                tiles[nearest.x, nearest.y].SetDebugColor(Color.yellow);
            yield return new WaitForSeconds(thinkTimeStep);

            if (nearest == targetNode)
            {
                path = ReconstructPath(nearest, debug);
                break;
            }

            foreach (GraphEdge edge in nearest.edges)
            {
                float newPathCost = nearest.pathCost + edge.travelCost;
                if (newPathCost < edge.to.pathCost)
                {
                    edge.to.pathCost = newPathCost;
                    edge.to.previous = nearest;
                    if (debug)
                        tiles[edge.to.x, edge.to.y].SetDebugColor(Color.cyan);
                    yield return new WaitForSeconds(thinkTimeStep);

                }
            }
        }
        yield return null;
    }

    public List<PathNode> ReconstructPath(GraphNode current, bool debug)
    {
        List<PathNode> path = new List<PathNode>();

        path.Add(current);
        if (debug)
            tiles[current.x, current.y].SetDebugColor(Color.red);
        while (current.previous != null)
        {
            current = current.previous;
            path.Add(current);
            if (debug)
                tiles[current.x, current.y].SetDebugColor(Color.red);
        }
        path.Reverse();
        return path;
    }
}
