using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode {NoiseMap, ColorMap, Mesh};
    public DrawMode drawMode;

    const int mapChunkSize = 241;
    [Range(0, 6)]
    public int levelOfDetail;
    public float noiseScale;
    public bool autoUpdate;

    public int octaves = 1;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;

    
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap
            (
            mapChunkSize, mapChunkSize, seed, offset, 
            noiseScale, octaves, persistance, lacunarity
            );

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight<= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            MeshData m = MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail);
            Texture2D t = TextureGenerator.TextureFromColourMap(colorMap, mapChunkSize, mapChunkSize);
            display.DrawMesh(m,t);
        }
    }
    private void Start()
    {
        GenerateMap();
    }

    private void OnValidate()
    {
       
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}
[System.Serializable]
public struct TerrainType
{
    public string name;
    [Range(0,1)]
    public float height;
    public Color color;
}