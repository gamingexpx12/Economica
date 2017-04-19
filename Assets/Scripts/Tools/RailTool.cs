using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will become a child tool object that encapsulates rail building.
/// </summary>
[RequireComponent(typeof(Ghosting))]
public class RailTool : MonoBehaviour {
    public GameSettings gameSettings;
    public Vector3 cursorPosition;
    public Vector3 cursorDragStart;
    public Vector3 cursorPositionWithinTile;
    public GameObject cursorObject;
    public LineData lineData;
    [EnumFlag]
    public TrackDirection direction = TrackDirection.NS | TrackDirection.EW;

    public string UseButton;
    /// <summary>
    /// Terrain Layer
    /// </summary>
    int _layerMask = 1 << 8;
    LineDrawer _lineDrawer;
    Ghosting _ghosting;
    float _raycastDistance;

    private void Start()
    {
        _raycastDistance = gameSettings.ScreenToWorldRaycastLength;
        _lineDrawer = GetComponent<LineDrawer>();
        _lineDrawer.model = cursorObject;
        _lineDrawer.grid = gameSettings.GridSize;

        _ghosting = GetComponent<Ghosting>();
    }

    private void Update()
    {
        transform.position = cursorPosition;
        _lineDrawer.inTilePositon = cursorPositionWithinTile;

        if (!Input.GetButton(UseButton))
        {
            cursorDragStart = cursorPosition;
        }

        //Choose which drawing method to use.
        if (SharedLibrary.VectorLocationEqual(cursorDragStart, cursorPosition))
        {
            lineData = Selection.SelectTile(cursorPosition, cursorPositionWithinTile);
        }
        else
        {
            lineData = _lineDrawer.MakeLine(cursorDragStart, cursorPosition);
        }
        //_ghosting.Ghost(cursorObject, lineData.direction, lineData.instances);
        _ghosting.CreateGhostTrack(cursorObject, lineData.trackDirection, lineData.instances);

    }

    private void FixedUpdate () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _raycastDistance, _layerMask))
        {

            cursorPosition = _SnapToGrid(hit.point, gameSettings.GridSize);
            cursorPositionWithinTile = hit.point - cursorPosition;
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

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < lineData.instances.Length; i++)
        {
            Gizmos.DrawSphere(lineData.instances[i], 0.3f);
        }
    }
}
