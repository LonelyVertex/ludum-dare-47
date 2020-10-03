using UnityEngine;

public class EnemyWayPoints : MonoBehaviour
{
    public WayPointCollection wayPointCollection;
    public float reachThreshold;

    private int _currentWayPoint = 0;
    private int _countDirection = 1;

    public Transform CurrentWayPoint => wayPointCollection.wayPoints[_currentWayPoint];

    public bool HasReachedWayPoint =>
        Vector3.Distance(transform.position, wayPointCollection.wayPoints[_currentWayPoint].position) <= reachThreshold;

    public Transform NextWayPoint()
    {
        if (wayPointCollection.wayPoints.Count == 0) return null;

        IncrementCurrentWayPoint();
        return wayPointCollection.wayPoints[_currentWayPoint];
    }

    private void IncrementCurrentWayPoint()
    {
        if (wayPointCollection.mode == WayPointCollection.Mode.Loop)
        {
            IncrementCurrentWayPointLoop();
        }
        else if (wayPointCollection.mode == WayPointCollection.Mode.PingPong)
        {
            IncrementCurrentWayPointPingPong();
        }
    }

    private void IncrementCurrentWayPointLoop()
    {
        _currentWayPoint = (_currentWayPoint + 1) % wayPointCollection.wayPoints.Count;
    }

    private void IncrementCurrentWayPointPingPong()
    {
        if (_currentWayPoint + _countDirection >= wayPointCollection.wayPoints.Count)
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