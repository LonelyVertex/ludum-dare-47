using UnityEditor;
using UnityEngine;
using Zenject;

public class EnemyVision : MonoBehaviour
{
    public EnemyNavigation navigation;
    [Range(0, 360)] public float fov;
    public float viewDistance;

    [Inject] private Player _player;

    private Vector3 Direction => navigation.navMeshAgent.velocity.x < 0 ? -transform.right : transform.right;

    public bool CanSeePlayer
    {
        get
        {
            if (_player == null) return false;

            var distance = Vector2.Distance(transform.position, _player.transform.position);
            var inViewRange = distance <= viewDistance;

            var angle = Vector2.Angle(_player.transform.position - transform.position, Direction);
            var inViewAngle = angle <= fov / 2;

            if (!inViewRange || !inViewAngle) return false;

            var hit = Physics2D.Raycast(transform.position, _player.transform.position - transform.position,
                viewDistance);

            return hit.transform != null && hit.transform.CompareTag("Player");
        }
    }

    void OnDrawGizmos()
    {
        // Vision
        Handles.color = new Color(1, 1, 1, 0.05f);
        var forward = Quaternion.AngleAxis(-fov / 2, transform.forward) * Direction;
        Handles.DrawSolidArc(transform.position, transform.forward, forward, fov, viewDistance);

        // Player
        if (_player != null)
        {
            Handles.color = CanSeePlayer ? new Color(1, 0, 0, 0.5f) : new Color(1, 1, 1, 0.2f);
            Handles.DrawLine(transform.position, _player.transform.position);
        }
    }
}