using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavigation : MonoBehaviour
{
    public Health health;
    public NavMeshAgent navMeshAgent;
    public SpriteRenderer spriteRenderer;
    
    private Transform _target;
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        
    }

    private void Update()
    {
        if (!health.IsAlive)
        {
            navMeshAgent.isStopped = true;
            return;
        }
        
        if (_target != null)
        {
            navMeshAgent.SetDestination(_target.position);
            RotateTowardsDestination();
        }
    }

    private void RotateTowardsDestination()
    {
        if (_target != null)
        {
            spriteRenderer.flipX = (_target.transform.position - transform.position).x > 0;
        }
        else
        {
            spriteRenderer.flipX = navMeshAgent.velocity.x > 0;
        }
    }
}
