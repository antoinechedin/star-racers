using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathfindingEngine
{
    public List<PathNode> path;

    public abstract IEnumerator FindPath(Vector2 start, Vector2 target, bool debug);
}
