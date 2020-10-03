using UnityEditor;
using UnityEngine;
using Zenject;

public class EnemyVision : MonoBehaviour
{
    [Range(0, 360)] public float fov;
    public float viewDistance;

    [Inject] private Player _player;

    public bool CanSeePlayer
    {
        get
        {
            var distance = Vector2.Distance(transform.position, _player.transform.position);
            var inViewRange = distance <= viewDistance;
            
            var angle = Vector2.Angle(_player.transform.position - transform.position, transform.right);
            var inViewAngle = angle <= (fov / 2);

            return inViewRange && inViewAngle;
        }
    }

    void OnDrawGizmos()
    {
        // Vision
        Handles.color = new Color(1, 1, 1, 0.05f);
        var forward = Quaternion.AngleAxis(-fov / 2, transform.forward) * transform.right;
        Handles.DrawSolidArc(transform.position, transform.forward, forward, fov, viewDistance);

        // Player
        if (_player != null)
        {
            Handles.color = CanSeePlayer ? new Color(1, 0, 0, 0.5f) : new Color(1, 1, 1, 0.2f);
            Handles.DrawLine(transform.position, _player.transform.position);
        }
    }
}