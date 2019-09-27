using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : PathNode
{
    public int x, y;
    public Vector3 worldPosition;
    public float travelCost, gCost, fCost;
    public Node previousNode;

    public Node(int x, int y, Tile tile, RacerEquipement equipement)
    {
        this.x = x;
        this.y = y;
        this.worldPosition = tile.transform.position;
        this.travelCost = tile.travelCost / equipement.GetSpeedFactorFor(tile.type);
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
