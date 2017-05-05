using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathDebugger : MonoBehaviour {

    public Node node;
    public List<Node> neighbours;
    Grid _grid;
	// Use this for initialization
	void Start () {
        _grid = GameObject.FindGameObjectWithTag("Pathing").GetComponent<Grid>();
	}
	
	// Update is called once per frame
	void Update () {
        node = _grid.NodeFromWorldPoint(transform.position);
        neighbours = _grid.GetNeighbours(node);
	}

    private void OnDrawGizmosSelected()
    {
        DrawCube(node.worldPosition, Color.green);

        foreach (Node n in neighbours)
        {
            DrawCube(n.worldPosition, Color.yellow);
        }
    }

    void DrawCube(Vector3 center, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(center, Vector3.one * 4 );
    }
}
