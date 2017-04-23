using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for snapping a train to the track.
/// </summary>
public class TrackMovement : MonoBehaviour
{
    public Transform target;
    public float speed;
    public Vector3 direction;
    public List<Transform> waypoints;
    Queue<Transform> _waypointQueue;

    public void Start()
    {
        _waypointQueue = new Queue<Transform>();
        foreach (Transform waypoint in waypoints)
        {
            _waypointQueue.Enqueue(waypoint);
        }
    }

    public void Update()
    {
        if (_waypointQueue.Count == 0 & target == null) return;
        if (target.position == transform.position) target = null;
        _SetTarget();
        _MoveTowards(target);
        _LookTowards(target, 1);
    }

    void _LookTowards(Transform target, float minDistance)
    {
        if (target == null) return;
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > minDistance & target)
        {
            transform.LookAt(target);
        }
    }

    void _MoveTowards (Transform target)
    {
        float step = speed * Time.deltaTime;
        
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }        
    }

    void _SetTarget ()
    {
        if (target == null && _waypointQueue.Count > 0)
        {
            target = _waypointQueue.Dequeue();
        }  
    }
}
