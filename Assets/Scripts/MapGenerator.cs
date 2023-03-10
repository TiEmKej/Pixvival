using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public RuleTile landTile;
    public Tile waterTile;
    public int mapSize = 400;
    public float noiseScale = 0.05f;
    public float thresholdLand = 0.35f;
    public float thresholdTreeMax = 0.65f;
    public float thresholdTreeMin = 0.55f;
    public int islandRadius = 175;
    public Object treeObject;
    public Transform treeGroup;

    public string seed = "0";
    public int curSeed;

    private float[,] noiseMap;

    void Start()
    {
        //curSeed = seed.GetHashCode();
        //Random.InitState(curSeed);
        noiseMap = new float[mapSize, mapSize];
        GenerateNoiseMap();
        GenerateMap();
        GenerateTree();
    }
    void GenerateNoiseMap()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                noiseMap[x, y] = Mathf.PerlinNoise(((float)x + curSeed) * noiseScale, ((float)y + curSeed) * noiseScale);
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
                    tilemap.SetTile(new Vector3Int(x - mapSize / 2, y - mapSize / 2, 0), waterTile);
                }
                else if (noiseMap[x, y] < thresholdLand)
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

    void GenerateTree()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                if (noiseMap[x, y] > thresholdTreeMin && noiseMap[x, y] > thresholdTreeMax && tilemap.GetTile(new Vector3Int(x - mapSize / 2, y - mapSize / 2, 0)) == landTile)
                {
                    Instantiate(treeObject, new Vector3(x - mapSize / 2, y - mapSize / 2, 0), Quaternion.identity, treeGroup);
                }
            }
        }
    }
}


