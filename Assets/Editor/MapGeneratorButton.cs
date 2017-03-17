using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorButton : Editor {

	public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target; //Get reference to this map generator

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.DrawMapInEditor();
            }
        }

        if (GUILayout.Button ("Generate"))
        {
            mapGen.DrawMapInEditor();
        }
    }
}
