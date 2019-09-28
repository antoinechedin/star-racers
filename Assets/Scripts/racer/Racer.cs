using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racer : MonoBehaviour
{
    public PathfindingEngine pathEngine;
    public List<PathNode> path = new List<PathNode>();
    public RacerEquipement equipement;
    bool moving = false;

    public void FindPathAndDrive(Vector2 target, bool debug = false)
    {
        path = pathEngine.FindPath(transform.position, target, debug);
    }

    IEnumerator Move(Vector3 to, float duration)
    {
        float d = duration / RaceManager.raceSpeed;
        Vector3 start = transform.position;
        transform.rotation = Quaternion.Euler(
            0f,
            0f,
            Vector3.SignedAngle(Vector3.up, to - start, Vector3.forward)
        );
        float t = 0;
        while (t < d)
        {
            t += Time.deltaTime;
            t = t > d ? d : t;
            transform.position = Vector3.Lerp(start, to, t / d);
            yield return null;
        }
        moving = false;
        path.RemoveAt(0);
        yield return null;
    }

    void Update()
    {
        if (!moving && path.Count >= 2)
        {
            moving = true;
            StartCoroutine(Move(path[1].GetWorldPosition(), (path[0].GetTravelCost() + path[1].GetTravelCost()) / 2f));
        }
    }
}
