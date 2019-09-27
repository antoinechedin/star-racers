using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
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
}
