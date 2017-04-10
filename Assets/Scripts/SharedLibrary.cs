using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SharedLibrary
{
    public static readonly Vector3 North = new Vector3(1, 0, 0);
    public static readonly Vector3 South = new Vector3(-1, 0, 0);
    public static readonly Vector3 East = new Vector3(0, 0, -1);
    public static readonly Vector3 West = new Vector3(0, 0, 1);

    /// <summary>
    /// Compares x and z values of a vector. Z is ignored.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool VectorLocationEqual(Vector3 a, Vector3 b)
    {
        bool xval = a.x == b.x;
        bool zval = a.z == b.z;
        if (xval & zval)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static Vector3 CardinalDirection(Vector3 direction, bool CanZero = false)
    {
        float north = direction.x >= 0 ? direction.x : 0;
        float south = direction.x <  0 ? Mathf.Abs(direction.x) : 0;
        float west  = direction.z >= 0 ? direction.z : 0;
        float east  = direction.z <  0 ? Mathf.Abs(direction.z) : 0;
        if (north > west & north > east)
        {
            return North;
        }
        if (south > west & south > east)
        {
            return South;
        }
        if (west > north & west > south)
        {
            return West;
        }
        if (east > north & east > south)
        {
            return East;
        }

        //Default
        if (CanZero)
        {
            return Vector3.zero;
        }
        return North;
    }
}
[System.Serializable]
public struct LineData
{
    public Vector3[] instances;
    public Quaternion direction;
}