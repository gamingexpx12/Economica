using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the camera along one axis.
/// </summary>
public class CameraMover : MonoBehaviour {
    public enum axis { x, y, z};

    public axis direction = axis.z;

    [Range(0, 100)]
    public float speed = 10f;

	void Update () {
        float x = direction == axis.x ? speed * Time.deltaTime : 0;
        float y = direction == axis.y ? speed * Time.deltaTime : 0;
        float z = direction == axis.z ? speed * Time.deltaTime : 0;
        transform.position += new Vector3(x, y, z);
	}
}
