using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static float raceSpeed = 2f;

    public Texture2D level;
    public GameObject defaultTile;
    public ColorMapper mapper;

    Tile[,] tiles;
    Tile finishTile;

    public GameObject carPrefab;
    public GameObject truckPrefab;
    public GameObject playerPrefab;

    private void Start()
    {
        BuildLevel();
        
        Racer enemy1 = Instantiate(
            carPrefab, 
            new Vector2(1f + .5f, 0f + .5f), 
            Quaternion.identity
        ).GetComponent<Racer>();
        enemy1.pathEngine = new DijkstraEngine(tiles, enemy1.equipement);
        StartCoroutine(enemy1.FindPathAndDrive(finishTile.transform.position));

        Racer enemy3 = Instantiate(
            truckPrefab, 
            new Vector2(6f + .5f, 0f + .5f), 
            Quaternion.identity
        ).GetComponent<Racer>();
        enemy3.pathEngine = new DijkstraEngine(tiles, enemy3.equipement);
       StartCoroutine(enemy3.FindPathAndDrive(finishTile.transform.position));

        Racer player = Instantiate(
            playerPrefab, 
            new Vector2(4f + .5f, 0f + .5f), 
            Quaternion.identity
        ).GetComponent<Racer>();
        player.pathEngine = new AStarEngine(tiles, player.equipement);
        StartCoroutine(player.FindPathAndDrive(finishTile.transform.position, false));
    }

    void BuildLevel()
    {
        tiles = new Tile[level.width, level.height];
        for (int x = 0; x < level.width; x++)
        {
            for (int y = 0; y < level.height; y++)
            {
                tiles[x, y] = InstantiateTile(x, y, level.GetPixel(x, y));
                if (tiles[x, y].isFinish)
                {
                    finishTile = tiles[x, y];
                }
            }
        }
    }

    Tile InstantiateTile(float x, float y, Color type)
    {
        foreach (ColorMapping mapping in mapper.mapping)
        {
            if (type == mapping.color)
            {
                return Instantiate(
                    mapping.prefab,
                    new Vector2(x + .5f, y + .5f),
                    Quaternion.identity,
                    transform
                ).GetComponent<Tile>();
            }
        }
        return Instantiate(
            defaultTile,
            new Vector2(x + .5f, y + .5f),
            Quaternion.identity
        ).GetComponent<Tile>();
    }
}
