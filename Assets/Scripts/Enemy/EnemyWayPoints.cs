using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWayPoints : MonoBehaviour
{
    public enum Mode
    {
        Loop = 0,
        PingPong = 1,
    }

    public List<Transform> wayPoints;
    public float reachThreshold;
    public Mode mode;

    private int _currentWayPoint = 0;
    private int _countDirection = 1;

    public Transform CurrentWayPoint => wayPoints[_currentWayPoint];

    public bool HasReachedWayPoint =>
        Vector3.Distance(transform.position, wayPoints[_currentWayPoint].position) <= reachThreshold;

    public Transform NextWayPoint()
    {
        if (wayPoints.Count == 0) return null;

        IncrementCurrentWayPoint();
        return wayPoints[_currentWayPoint];
    }

    private void IncrementCurrentWayPoint()
    {
        if (mode == Mode.Loop)
        {
            IncrementCurrentWayPointLoop();
        }
        else if (mode == Mode.PingPong)
        {
            IncrementCurrentWayPointPingPong();
        }
    }

    private void IncrementCurrentWayPointLoop()
    {
        _currentWayPoint = (_currentWayPoint + 1) % wayPoints.Count;
    }

    private void IncrementCurrentWayPointPingPong()
    {
        if (_currentWayPoint + _countDirection >= wayPoints.Count)
        {
            _countDirection = -1;
        }
        else if (_currentWayPoint + _countDirection < 0)
        {
            _countDirection = 1;
        }

        _currentWayPoint += _countDirection;
    }
}