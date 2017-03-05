using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heightmap : MonoBehaviour {

    public int seed;
    public float[,] map;
    public int height;
    public int width;

    private void OnDrawGizmos()
    {
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                Gizmos.color = map[x, y] >= 0.5 ? Color.black : Color.white;
                Gizmos.DrawCube(new Vector3(transform.position.x + x, transform.position.y + y), Vector3.one);
            }
        }

    }

    private void OnValidate()
    {
        height = height < 0 ? 0 : height;
        width = width < 0 ? 0 : width;
        GenerateHeightmap();
    }

    private void GenerateHeightmap()
    {
        Random.InitState(seed);
        map = new float[height + 1, width + 1]; //inclusive
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                map[x, y] = Random.value;
            }
        }
    }
}
