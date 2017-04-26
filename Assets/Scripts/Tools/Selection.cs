using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Selection {
    public static GameSettings settings;
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

    public static LineData SelectLine(Vector3 begin, Vector3 end, int grid)
    {
        LineData line = new LineData();
        var localEnd = end - begin;
        int furthestAxis = Mathf.RoundToInt(Mathf.Max(Mathf.Abs(localEnd.x), Mathf.Abs(localEnd.z)));
        var distanceTile = furthestAxis / grid;
        var distanceReal = furthestAxis;
        var snappedEnd = begin + SharedLibrary.SnapToCardinal(localEnd, distanceReal);

        var ghosts = new Vector3[1];
        ghosts[0] = begin;
        var numInstances = ghosts.Length;

        line.instances = ghosts;
        //line.direction = SharedLibrary.GetDirection(begin, snappedEnd);
        line.trackDirection = SharedLibrary.GetTrackDirection(localEnd);
        return line;
    }

    public static LineData SelectArea(Vector3 beginPosition, Vector3 endPosition)
    {
        throw new System.NotImplementedException();
    }
}
