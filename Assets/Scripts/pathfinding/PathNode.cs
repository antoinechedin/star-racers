using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathNode
{
    public abstract Vector3 GetWorldPosition();
    public abstract float GetTravelCost();
}
