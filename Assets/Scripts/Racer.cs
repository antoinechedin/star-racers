using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racer : MonoBehaviour
{
    Pathfinding pathfinding;
    List<Tile> path;
    Tile finishTile;

    public void Initialize(Pathfinding pathfinding, Tile finishTile)
    {
        this.pathfinding = pathfinding;
        path = new List<Tile>();
        this.finishTile = finishTile;
    }

    public IEnumerator Drive()
    {
        path = pathfinding.FindPath(transform.position, finishTile.transform.position);
        path.RemoveAt(0);
        while (path.Count > 0)
        {
            Tile target = path[0];
            yield return StartCoroutine(Move(target.transform.position, .3f));
            path.RemoveAt(0);
        }
        transform.rotation = Quaternion.identity;
        yield return null;
    }

    IEnumerator Move(Vector3 to, float duration)
    {
        Vector3 start = transform.position;
        transform.rotation = Quaternion.Euler(0f, 0f, Vector3.SignedAngle(Vector3.up, to - start, Vector3.forward));
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t;
            transform.position = Vector3.Lerp(start, to, t / duration);
            yield return null;
        }
        yield return null;
    }
}
