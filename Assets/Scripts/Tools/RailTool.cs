using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailTool : MonoBehaviour {
    public Vector3 cursorPosition;
    public GameObject cursorObject;
    public Material ghostMaterial;

    public string UseButton;
    bool _useKeyHeld;
    /// <summary>
    /// Terrain Layer
    /// </summary>
    int _layerMask = 1 << 8;
    LineDrawer _lineDrawer;

    private void Start()
    {
        _lineDrawer = GetComponent<LineDrawer>();
        GameObject cursor = Instantiate(cursorObject, transform);
        MeshRenderer[] meshes = cursor.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer m in meshes)
        {
            m.material = ghostMaterial;
        }
        
    }

    private void Update()
    {
        if (Input.GetButtonDown(UseButton))
        {
            _lineDrawer.begin = cursorPosition;
        }
    }

    private void FixedUpdate () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f, _layerMask))
        {

            transform.position = _RoundVector(hit.point, 2);
        }
	}

    private Vector3 _RoundVector(Vector3 vector, int gridStep)
    {
        int x = RoundNum(Mathf.RoundToInt(vector.x), gridStep);
        int y = RoundNum(Mathf.RoundToInt(vector.y), gridStep);
        int z = RoundNum(Mathf.RoundToInt(vector.z), gridStep);

        return new Vector3(x, y, z);

    }

    private float _RoundNum(float num, float step)
    {
        if (num >= 0)
            return Mathf.Floor((num + step / 2) / step) * step;
        else
            return Mathf.Ceil((num - step / 2) / step) * step;
    }
    private int RoundNum(int num, int step)
    {
        if (num >= 0)
            return ((num + (step / 2)) / step) * step;
        else
            return ((num - (step / 2)) / step) * step;
    }
}
