using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public RuleTile landTile;
    public Tile waterTile;
    public int mapSize = 400;
    public float noiseScale = 0.05f;
    public float threshold = 0.35f;
    public int islandRadius = 175;

    private float[,] noiseMap;

    void Start()
    {
        noiseMap = new float[mapSize, mapSize];
        GenerateNoiseMap();
        GenerateMap();
        CheckConstantTile();
    }
    void GenerateNoiseMap()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                float distanceFromCenter = Vector2.Distance(new Vector2(x, y), new Vector2(mapSize / 2, mapSize / 2));
                noiseMap[x, y] = Mathf.PerlinNoise((float)x * noiseScale, (float)y * noiseScale);
            }
        }
    }

    void GenerateMap()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                float distanceFromCenter = Vector2.Distance(new Vector2(x, y), new Vector2(mapSize / 2, mapSize / 2));

                if (distanceFromCenter > islandRadius)
                {
                    tilemap.SetTile(new Vector3Int(x-mapSize/2, y-mapSize/2, 0), waterTile);
                }
                else if (noiseMap[x, y] < threshold)
                {
                    tilemap.SetTile(new Vector3Int(x - mapSize / 2, y - mapSize / 2, 0), waterTile);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x - mapSize / 2, y - mapSize / 2, 0), landTile);
                }
            }
        }
    }
    void CheckConstantTile()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                TileBase tempTile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tempTile == waterTile)
                {
                    if (tilemap.GetTile(new Vector3Int(x + 1, y, 0)) == landTile && tilemap.GetTile(new Vector3Int(x - 1, y, 0)) == landTile && tilemap.GetTile(new Vector3Int(x, y - 1, 0)) == landTile && tilemap.GetTile(new Vector3Int(x, y + 1, 0)) == landTile)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), landTile);
                    }
                }
            }
        }
    }
}
