using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile : MonoBehaviour
{
    [HideInInspector] public int gridX, gridY;
    [HideInInspector] public float gCost, fCost;

    public bool isPassable;

    [HideInInspector] public Tile previousNode;

    public void Initialise(int gridX, int gridY)
    {
        this.gridX = gridX;
        this.gridY = gridY;
        this.gCost = Mathf.Infinity;
        this.fCost = Mathf.Infinity;
    }
}
