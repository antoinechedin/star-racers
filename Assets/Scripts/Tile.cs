using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Type {none, road, grass, mountain, finish, start};

    public Type type;
    public float travelCost = 1f;
}
