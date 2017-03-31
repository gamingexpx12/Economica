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

    public Vector3 begin;
    public Vector3 end;

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
