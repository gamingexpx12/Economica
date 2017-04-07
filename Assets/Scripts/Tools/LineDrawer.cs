using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghosting))]
public class LineDrawer: MonoBehaviour
{
    public int distance;
    public int numInstances;
    public int grid = 2;
    public Vector3 inTilePositon;
    Vector3[] _ghosts;
    Ghosting _ghoster;
    GameObject _model;

    public Vector3 begin
    {
        get { return _begin; }
        set { _begin = value; }
        
    }
    public Vector3 end
    {
        get { return _end; }
        set { _end = value; }

    }

    public GameObject model
    {
        get { return _model; }
        set { _model = value; }
    }

    Vector3 _begin;
    Vector3 _end;
    Quaternion _direction;
    public Vector3 _diff;

    public void DrawLine()
    {
        Debug.DrawLine(begin, end);

    }
    private void Start()
    {
        _ghoster = GetComponent<Ghosting>();
    }

    private void Update()
    {
        _diff = _end - _begin;
        int furthestAxis = Mathf.RoundToInt(Mathf.Max(Mathf.Abs(_diff.x), Mathf.Abs(_diff.z)));
        distance = furthestAxis / grid;
        _SetInstances();
        numInstances = _ghosts.Length;
        _direction = _GetDirection();
        _ghoster.Ghost(_model, _direction, _ghosts);
        print(inTilePositon);

    }
    

    private void OnDrawGizmosSelected()
    {
        if (_ghosts == null)
        {
            return;
        }
        Gizmos.DrawCube(begin, Vector3.one);
        Gizmos.DrawCube(end, Vector3.one);
        Gizmos.DrawLine(begin, end);
        
        for (int i = 0; i < _ghosts.Length; i++)
        {
            Gizmos.DrawSphere(_ghosts[i], 0.3f);
        }
    }

    private void _SetInstances()
    {
        if (SharedLibrary.VectorLocationEqual(_begin, _end))
        {
            _ghosts = new Vector3[1];
            _ghosts[0] = _begin;
            return;
        }
        int dist = distance + 1;
        _ghosts = new Vector3[dist];
        for (int tile = 0; tile < dist; tile++)
        {
            float tilePosF = (float)tile / (float)distance;
            Vector3 tilePos = Vector3.Lerp(_begin, _end, tilePosF);
            _ghosts[tile] = tilePos;

            
        }
    }

    private Quaternion _GetDirection()
    {
        var result = new Quaternion();
        if (SharedLibrary.VectorLocationEqual(_begin, _end))
        {
            var cardinal = SharedLibrary.CardinalDirection(inTilePositon);
            result.SetLookRotation(cardinal);
            return result;
        }
        else
        {
            result.SetLookRotation(_diff);
            return result;
        }
        
        
    }
}
