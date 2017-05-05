using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public Transform target;
    public float speed = 5.0F;
    public Vector3[] path;
    public bool LookForPath;
    int targetIndex;

    public void OnPathFound(Vector3[] newpath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newpath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else
        {
            print("No path found");
        }
    }

    public IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector3[0];
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            transform.LookAt(currentWaypoint);
            yield return null;
        }
    }

    void Update()
    {
        if (LookForPath & path.Length >= 0)
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            LookForPath = false;
        }
    }

    void OnDrawGizmos()
    {
        if (path != null)
        {
            Gizmos.color = Color.black;
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.DrawWireCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}