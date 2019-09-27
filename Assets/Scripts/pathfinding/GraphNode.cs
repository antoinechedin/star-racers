using System.Collections.Generic;
using UnityEngine;

public class GraphNode
{
    public int x, y;
    public Vector3 worldPosition;
    public float pathCost;
    public List<GraphEdge> edges;
    public GraphNode previous;

    public GraphNode(int x, int y, Vector3 worldPosition)
    {
        this.x = x;
        this.y = y;
        this.worldPosition = worldPosition;
        this.edges = new List<GraphEdge>();
    }
}
