using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Selection {

    public static LineData SelectTile(Vector3 worldPosition, Vector3 inTilePosition)
    {
        LineData sqareData = new LineData();

        var direction = new Quaternion();
        var cardinal = SharedLibrary.CardinalDirection(inTilePosition, SharedLibrary.North);
        direction.SetLookRotation(cardinal);
        sqareData.direction = direction;
        sqareData.trackDirection = SharedLibrary.CardinalToTrackDirection(cardinal);

        var instance = new Vector3[1];
        instance[0] = worldPosition;
        sqareData.instances = instance;
        return sqareData;
    }

    public static LineData SelectLine(Vector3 beginPosition, Vector3 endPosition)
    {
        throw new System.NotImplementedException();
    }

    public static LineData SelectArea(Vector3 beginPosition, Vector3 endPosition)
    {
        throw new System.NotImplementedException();
    }
}
