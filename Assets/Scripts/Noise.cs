using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidthEnd, int mapHeightEnd, float scale) //Exclusive - Will have one less than provided
    {
        float[,] noiseMap = new float[mapWidthEnd, mapHeightEnd];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }
        for (int y = 0; y < mapHeightEnd; y++)
        {
            for (int x = 0; x < mapWidthEnd; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap [x, y] = perlinValue;
            }
        }

        return noiseMap;
    }
}
