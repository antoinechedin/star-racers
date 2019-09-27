using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathfindingEngine
{
    public abstract List<PathNode> FindPath(Vector2 start, Vector2 target);
}
