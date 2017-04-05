using System;
using System.Collections.Generic;
using UnityEngine;


public class LineDrawer: MonoBehaviour
{
    public int distance;
    public int numInstances;
    public int grid = 2;

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

    Vector3 _begin;
    Vector3 _end;
    public Vector3 _diff;

    public void DrawLine()
    {
        Debug.DrawLine(begin, end);

    }

    private void Update()
    {
        _diff = _end - _begin;
        int furthestAxis = Mathf.RoundToInt(Mathf.Max(Mathf.Abs(_diff.x), Mathf.Abs(_diff.z)));
        distance = furthestAxis / grid;

       
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(begin, Vector3.one);
        Gizmos.DrawCube(end, Vector3.one);
        Gizmos.DrawLine(begin, end);
        int dist = distance;
        if (_begin == _end)
        {
            Gizmos.DrawSphere(_begin, 0.3f);
            return;
        }
        for (int tile = 0; tile < distance + 1; tile++)
        {
            float tilePosF = (float)tile / (float)distance;
            Vector3 tilePos = Vector3.Lerp(_begin, _end, tilePosF);
            Gizmos.DrawSphere(tilePos, 0.3f);
            print(tilePosF);
        }
    }
}
