using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Texture2D map;
    public Tile defaultTile;
    public ColorMapping[] colorMappings;

    GameGrid grid;
    
    List<Tile> path;

    private void Start()
    {
        GenerateLevel();
        path = new List<Tile>();

    }

    private void Update()
    {
        if (path.Count > 0)
        {
            foreach (Tile tile in path)
            {
                tile.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        path.Clear();


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        Pathfinding pf = new Pathfinding(grid, false);
        path = pf.FindPath(Vector2.zero, mousePosition);

        foreach (Tile tile in path)
        {
            tile.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void GenerateLevel()
    {
        grid = new GameGrid(new Vector2Int(map.width, map.height));
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                grid.tileMatrix[x, y] = GenerateTile(x, y);
            }
        }
    }

    Tile GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a != 0)
        {
            foreach (ColorMapping colorMapping in colorMappings)
            {
                if (colorMapping.color.Equals(pixelColor))
                {
                    Tile obj = Instantiate(colorMapping.tilePrefab, new Vector2(x + .5f, y + .5f), Quaternion.identity, transform).GetComponent<Tile>();
                    obj.Initialise(x, y);
                    return obj;
                }
            }
        }
        Tile tile = Instantiate(defaultTile, new Vector2(x + .5f, y + .5f), Quaternion.identity, transform).GetComponent<Tile>();
        tile.Initialise(x, y);
        return tile;
    }

}
