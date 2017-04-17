using System;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour {
    public int railType;
    [EnumFlag("Track")]
    public TrackDirection track;
	// Use this for initialization
	void Start () {
		
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