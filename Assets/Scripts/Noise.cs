﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Generates noisemaps.
/// </summary>
public static class Noise
{
    public enum NormalizeMode { Local, Global};
    /// <summary>
    /// Method for making a noisemap. Should be thread safe.
    /// </summary>
    /// <param name="mapWidthEnd"></param>
    /// <param name="mapHeightEnd"></param>
    /// <param name="seed"></param>
    /// <param name="offset"></param>
    /// <param name="scale"></param>
    /// <param name="normalizeMode"></param>
    /// <param name="octaves">how many times it will run, each a smaller wave</param>
    /// <param name="persistance">How much will each octave matter</param>
    /// <param name="lacunarity"></param>
    /// <returns></returns>
    public static float[,] GenerateNoiseMap
        (
        int mapWidthEnd, 
        int mapHeightEnd, 
        int seed,
        Vector2 offset,
        float scale,
        NormalizeMode normalizeMode,
        int octaves = 4, 
        float persistance = 0.5f, 
        float lacunarity = 2
        
        )
    {
        float[,] noiseMap = new float[mapWidthEnd, mapHeightEnd];

        System.Random randomGen = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        float maxPossibleHeight = 0;
        float amplitude = 1;
        float frequency = 1;
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = randomGen.Next(-100000, 100000) + offset.x;
            float offsetY = randomGen.Next(-100000, 100000) - offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += amplitude;
            amplitude *= persistance;
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxLocalNoiseHeight = float.MinValue;
        float minLocalNoiseHeight = float.MaxValue;

        float halfWidth = mapWidthEnd / 2f;
        float halfHeight = mapHeightEnd / 2f;

        for (int y = 0; y < mapHeightEnd; y++)
        {
            for (int x = 0; x < mapWidthEnd; x++)
            {

                amplitude = 1;
                frequency = 1;
                float noiseHeight = 0;

                for (int o = 0; o < octaves; o++)
                {
                    float sampleX = (x - halfWidth  + octaveOffsets[o].x) / scale * frequency;
                    float sampleY = (y - halfHeight + octaveOffsets[o].y) / scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if (noiseHeight > maxLocalNoiseHeight)
                {
                    maxLocalNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minLocalNoiseHeight)
                {
                    minLocalNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeightEnd; y++)
        {
            for (int x = 0; x < mapWidthEnd; x++)
            {
                if (normalizeMode == NormalizeMode.Local)
                {
                    noiseMap[x, y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[x, y]);
                }
                else
                {
                    float normalizedHeight = (noiseMap[x, y] + 1) / (2f * maxPossibleHeight / 1.75f);
                    noiseMap[x, y] = Mathf.Clamp(normalizedHeight, 0, int.MaxValue);
                }
            }
        }

        return noiseMap;
    }
}
