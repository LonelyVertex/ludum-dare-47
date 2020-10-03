using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyNavigation))]
[RequireComponent(typeof(EnemyVision))]
[RequireComponent(typeof(EnemyWayPoints))]
public class SimpleEnemyController : MonoBehaviour
{
    [Inject] private Player _player;
    
    private EnemyNavigation _navigation;
    private EnemyVision _vision;
    private EnemyWayPoints _wayPoints;

    private bool _chasingPlayer;
    
    void Start()
    {
        _navigation = GetComponent<EnemyNavigation>();
        _vision = GetComponent<EnemyVision>();
        _wayPoints = GetComponent<EnemyWayPoints>();
        
        _navigation.SetTarget(_wayPoints.CurrentWayPoint);
    }

    void Update()
    {
        if (_chasingPlayer) return;
        
        if (_wayPoints.HasReachedWayPoint)
        {
            _navigation.SetTarget(_wayPoints.NextWayPoint());
        }
        
        if (_vision.CanSeePlayer)
        {
            _navigation.SetTarget(_player.transform);
            _chasingPlayer = true;
        }
    }
}
