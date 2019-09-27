using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Type {none, road, grass, mountain};

    public Type type;
    public float travelCost = 1f;
    public bool isStart, isFinish;
}
