using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SingleTileDrawer {

	public static LineData MakeSingleTile(Vector3 worldPosition, Vector3 inTilePosition)
    {
        LineData sqareData = new LineData();

        var direction = new Quaternion();
        var cardinal = SharedLibrary.CardinalDirection(inTilePosition);
        direction.SetLookRotation(cardinal);
        sqareData.direction = direction;

        var instance = new Vector3[1];
        instance[0] = worldPosition;
        sqareData.instances = instance;
        return sqareData;
    }
}
