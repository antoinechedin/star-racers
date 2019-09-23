using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorMapping[] colorMappings;

    private void Start() {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a != 0)
        {
            foreach (ColorMapping colorMapping in colorMappings)
            {
                if (colorMapping.color.Equals(pixelColor)){
                    // Instantiate tile grid
                }
            }
        }
    }

}
