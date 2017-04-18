using System;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour {
    public RailType railType;
    [EnumFlag("Track")]
    public TrackDirection track;
    public Transform mesh;
    [SerializeField]
    Transform[] meshObjects;
	
    public static GameObject MakeRailInstance(GameObject prefab, Transform parent, Vector3 position, TrackDirection direction)
    {
        var instance = Instantiate(prefab, position, new Quaternion(), parent);
        var railComp = instance.GetComponent<Rail>();
        railComp.track = direction;

        return instance;
    }

	void Start () {
        meshObjects = new Transform[railType.meshes.Length];
	}

    void OnValidate()
    {
        UpdateTrack();
    }
    void UpdateTrack()
    {
        bool containsNS = (track & TrackDirection.NS) != 0;
        bool containsEW = (track & TrackDirection.EW) != 0;
        bool containsNW = (track & TrackDirection.NW) != 0;
        bool containsNE = (track & TrackDirection.NE) != 0;
        bool containsSW = (track & TrackDirection.SW) != 0;
        bool containsSE = (track & TrackDirection.SE) != 0;

        meshObjects[0].gameObject.SetActive(containsNS);
        meshObjects[1].gameObject.SetActive(containsEW);
        meshObjects[2].gameObject.SetActive(containsNW);
        meshObjects[3].gameObject.SetActive(containsNE);
        meshObjects[4].gameObject.SetActive(containsSW);
        meshObjects[5].gameObject.SetActive(containsSE);
    }
}

[Flags]
[Serializable]
public enum TrackDirection
{
    NS = 1,
    EW = 2,
    NW = 4,
    NE = 8,
    SW = 16,
    SE = 32
}