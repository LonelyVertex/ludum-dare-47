using UnityEngine;
using Zenject;


public class SimpleEnemyController : MonoBehaviour
{
    [Inject] protected Player Player;

    [Header("Simple Enemy Controller ")] public EnemyNavigation navigation;
    public EnemyVision vision;
    public EnemyWayPoints wayPoints;

    protected bool ChasingPlayer;

    protected virtual void Start()
    {
        if (!ChasingPlayer)
        {
            navigation.SetTarget(wayPoints.CurrentWayPoint);
        }
    }

    protected virtual void Update()
    {
        if (ChasingPlayer) return;

        if (wayPoints.HasReachedWayPoint)
        {
            navigation.SetTarget(wayPoints.NextWayPoint());
        }

        if (vision.CanSeePlayer)
        {
            ChasePlayer();
        }
    }

    public void ChasePlayer()
    {
        navigation.SetTarget(Player.transform);
        ChasingPlayer = true;
    }
}