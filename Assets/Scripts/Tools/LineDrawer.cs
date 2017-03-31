using System;
using System.Collections.Generic;
using UnityEngine;


public class LineDrawer: MonoBehaviour
{
    public enum Axis
    {
        X,
        Z,
    }

    public Vector3 begin
    {
        get { return _begin; }
        set { print(value); _begin = value; }
        
    }
    public Vector3 end
    {
        get { return _end; }
        set { print(value); _end = value; }

    }

    Vector3 _begin;
    Vector3 _end;

    public void DrawLine()
    {
        Debug.DrawLine(begin, end);

    }

    private void Update()
    {
        //DrawLine();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(begin, Vector3.one);
        Gizmos.DrawCube(end, Vector3.one);
        Gizmos.DrawLine(begin, end);
    }
}
