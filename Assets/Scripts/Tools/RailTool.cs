using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailTool : MonoBehaviour {
    public Vector3 cursorPosition;
    public GameObject cursorObject;
    public Material ghostMaterial;

    public string UseButton;
    /// <summary>
    /// Terrain Layer
    /// </summary>
    int _layerMask = 1 << 8;
    LineDrawer _lineDrawer;

    private void Start()
    {
        _lineDrawer = GetComponent<LineDrawer>();
        _lineDrawer.begin = cursorPosition;
        _lineDrawer.end = cursorPosition;
        GameObject cursor = Instantiate(cursorObject, transform);
        MeshRenderer[] meshes = cursor.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer m in meshes)
        {
            m.material = ghostMaterial;
        }
        
    }

    private void Update()
    {
        transform.position = cursorPosition;
        _lineDrawer.end = cursorPosition;

        if (!Input.GetButton(UseButton))
        {
            _lineDrawer.begin = cursorPosition;
        }
    }

    private void FixedUpdate () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f, _layerMask))
        {

            cursorPosition = _SnapToGrid(hit.point, 2);
        }
	}

    private Vector3 _SnapToGrid(Vector3 location, int snap)
    {
        int x = snap * Mathf.RoundToInt(location.x / snap);
        int y = snap * Mathf.RoundToInt(location.y / snap);
        int z = snap * Mathf.RoundToInt(location.z / snap);

        //x = x % snap ;
        //print(x / snap);
        //x = snap* Mathf.Round(location.x / snap);
        return new Vector3(x, y, z);
    }
}
