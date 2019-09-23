using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    GameGrid grid;


    void FindPath(Vector2 start, Vector2 target)
    {
        Node startNode = grid.GetNodeFromWorldPostion(start);
        Node targetNode = grid.GetNodeFromWorldPostion(target);

        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();
        openSet.Add(startNode);

        while(openSet.Count > 0){
            Node node = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                
            }

        }
    }
}
