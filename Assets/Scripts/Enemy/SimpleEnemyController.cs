using UnityEngine;
using Zenject;


public class SimpleEnemyController : MonoBehaviour
{
    [Inject] protected Player Player;

    [Header("Simple Enemy Controller ")] public EnemyNavigation navigation;
    public EnemyVision vision;
    public EnemyWayPoints wayPoints;
    public Health health;

    public float chaseStoppingDistance;

    protected bool ChasingPlayer;

    protected virtual void Start()
    {
        health.healthDepletedEvent += OnHealthDepleted;
        
        if (!ChasingPlayer)
        {
            navigation.SetTarget(wayPoints.CurrentWayPoint);
        }
    }

    private void OnDestroy()
    {
        health.healthDepletedEvent -= OnHealthDepleted;
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

        if (chaseStoppingDistance > 0)
        {
            navigation.navMeshAgent.stoppingDistance = chaseStoppingDistance;
        }
    }

    private void OnHealthDepleted()
    {
        // TODO some fancy effects of dying
        Destroy(gameObject);
    }
}