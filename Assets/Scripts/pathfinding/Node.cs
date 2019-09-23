using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2 worldPosition;
    public int gridX;
    public int gridY;

    public bool selected;

    public float hCost;

    public Node previous;

    public Node(int gridX, int gridY, Vector2 worldPosition)
    {
        this.gridX = gridX;
        this.gridY = gridY;
        this.worldPosition = worldPosition;
    }
}
