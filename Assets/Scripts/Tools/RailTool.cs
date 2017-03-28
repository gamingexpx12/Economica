using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailTool : MonoBehaviour {
    /// <summary>
    /// Terrain Layer
    /// </summary>
    int _layerMask = 1 << 8;

	void FixedUpdate () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f, _layerMask))
        {
            transform.position = hit.point;
        }
	}
}
