using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heightmap : MonoBehaviour {

    public int seed;
    public float[,] map;

    public int x_;
    public int y_;

	private void Start ()
    {
		
	}
	

    private void OnDrawGizmos()
    {
        for (int x = 0; x < transform.localScale.x - 1; x++)
        {
            for (int y = 0; y < transform.localScale.y - 1; y++)
            {
                //Gizmos.color = height[x, y] == 1 ? Color.black : Color.white;
                Gizmos.DrawCube(new Vector3(transform.position.x + x, transform.position.y + y), Vector3.one);
            }
        }

    }

    private void OnValidate()
    {
        GenerateHeightmap();
    }

    private void GenerateHeightmap()
    {
        Random.InitState(seed);
        map = new float[(int)transform.localScale.x + 1, (int)transform.localScale.y + 1];
        for (int x = 0; x < transform.localScale.x; x++)
        {
            for (int y = 0; y < transform.localScale.y; y++)
            {
                map[x, y] = Random.Range(0, 1);
            }
        }
    }
}
