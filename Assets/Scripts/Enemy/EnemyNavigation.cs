using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavigation : MonoBehaviour
{
    public float rotationSpeed;
    
    private NavMeshAgent _navMeshAgent;
    private Transform _target;

    public Transform Target => _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        
    }

    private void Update()
    {
        if (_target != null)
        {
            _navMeshAgent.SetDestination(_target.position);
            RotateTowardsDestination();
        }
    }

    private void RotateTowardsDestination()
    {
        var velocity = _navMeshAgent.velocity;
        var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
