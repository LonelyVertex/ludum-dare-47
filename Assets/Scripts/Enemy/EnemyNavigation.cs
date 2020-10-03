using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavigation : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float rotationSpeed;
    
    private Transform _target;

    public Transform Target => _target;

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
        if (_target != null)
        {
            navMeshAgent.SetDestination(_target.position);
            RotateTowardsDestination();
        }
    }

    private void RotateTowardsDestination()
    {
        var velocity = navMeshAgent.velocity;
        var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
