using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SharedLibrary
{
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
}
