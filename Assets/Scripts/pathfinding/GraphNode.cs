using System.Collections.Generic;
using UnityEngine;

public class GraphNode : PathNode
{
    public int x, y;
    public Vector3 worldPosition;
    public float pathCost;
    public float travelCost;
    public List<GraphEdge> edges;
    public GraphNode previous;

    public GraphNode(int x, int y, Tile tile, RacerEquipement equipement)
    {
        this.x = x;
        this.y = y;
        this.worldPosition = tile.transform.position;
        this.edges = new List<GraphEdge>();
        travelCost = tile.travelCost / equipement.GetSpeedFactorFor(tile.type);
    }

    public override float GetTravelCost()
    {
        return travelCost;
    }

    public override Vector3 GetWorldPosition()
    {
        return worldPosition;
    }
}
