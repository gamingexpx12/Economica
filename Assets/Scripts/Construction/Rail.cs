using System;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour {
    public RailType railType;
    /// <summary>
    /// Only use in unity editor.
    /// </summary>
    [EnumFlag("Track")]
    public TrackDirection editorTrack;
    public Transform mesh;
    [SerializeField]
    Transform[] meshObjects;

    public TrackDirection track
    {
        get { return editorTrack;}
        set { editorTrack = value; UpdateTrack(); }
    }
	
    public static GameObject MakeRailInstance(GameObject prefab, Transform parent, Vector3 position, TrackDirection direction)
    {
        var instance = Instantiate(prefab, position, new Quaternion(), parent);
        var railComp = instance.GetComponent<Rail>();

        if (railComp == null) { throw new UnityException("Could not find Rail component"); }
        railComp.track = direction;

        return instance;
    }

	void Start () {
        
	}

    void OnValidate()
    {
        UpdateTrack();
    }
    void UpdateTrack()
    {
        bool containsNS = (editorTrack & TrackDirection.NS) != 0;
        bool containsEW = (editorTrack & TrackDirection.EW) != 0;
        bool containsNW = (editorTrack & TrackDirection.NW) != 0;
        bool containsNE = (editorTrack & TrackDirection.NE) != 0;
        bool containsSW = (editorTrack & TrackDirection.SW) != 0;
        bool containsSE = (editorTrack & TrackDirection.SE) != 0;

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