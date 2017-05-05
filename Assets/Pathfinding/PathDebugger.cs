using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDebugger : MonoBehaviour {

    public int x;
    public int y;
    public Node _node;
    Grid _grid;
	// Use this for initialization
	void Start () {
        _grid = GameObject.FindGameObjectWithTag("Pathing").GetComponent<Grid>();
	}
	
	// Update is called once per frame
	void Update () {
        _node = _grid.NodeFromWorldPoint(transform.position);
        x = _node.gridX;
        y = _node.gridY;
	}
}
