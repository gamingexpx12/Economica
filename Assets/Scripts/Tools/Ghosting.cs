using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosting : MonoBehaviour {

    public Material ghostMaterial;
    GameObject[] _ghosts;

    public void Ghost(GameObject modelPrefab, Quaternion direction, Vector3[] positions)
    {
        _RemoveGhosts();
        _ghosts = new GameObject[positions.Length];
        for (int p = 0; p < positions.Length; p++)
        {
            GameObject ghost = Instantiate(modelPrefab, positions[p], direction, transform);
            _ghosts[p] = ghost;

            MeshRenderer[] meshes = ghost.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer m in meshes)
            {
                m.material = ghostMaterial;
            }
        }

    }

    private void _RemoveGhosts()
    {
        if (_ghosts != null)
        {
            for (int ghost = 0; ghost < _ghosts.Length; ghost++)
            {
                Destroy(_ghosts[ghost]);
            } 
        }
    }
}
