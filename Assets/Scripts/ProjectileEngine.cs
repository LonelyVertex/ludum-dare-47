using System;
using UnityEngine;
using Zenject;

public class ProjectileEngine : MonoBehaviour
{
    private const string kObstacleTag = "Obstacle";

    public Rigidbody2D _rigidbody2D;
    public float projectileSpeed;
    public float lifespan;
    public float contactPointOffset;
    public ProjectileType projectileType;
    public event Action<ProjectileEngine, Vector3, Quaternion> killProjectile;

    private float _currentLifeSpan;
    private ProjectileType _projectileType;
    
    public class Pool : MonoMemoryPool<Vector3, Quaternion, ProjectileType, ProjectileEngine>
    {
        protected override void Reinitialize(Vector3 position, Quaternion rotation, ProjectileType projectileType, ProjectileEngine projectileEngine)
        {
            projectileEngine.Reinitialize(position, rotation, projectileType);
        }
    }

    private void Reinitialize(Vector3 position, Quaternion rotation, ProjectileType projectileType)
    {
        _rigidbody2D.position = position;
        _rigidbody2D.rotation = rotation.eulerAngles.z;

        _currentLifeSpan = lifespan;
        this.projectileType = projectileType;
    }
    
    private void Update()
    { 
        _currentLifeSpan -= Time.deltaTime;
        if (_currentLifeSpan <= 0)
        {
            killProjectile?.Invoke(this, Vector3.zero, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = transform.right * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(kObstacleTag))
        {
            var contact = other.GetContact(0);
            killProjectile?.Invoke(this, contact.point + contact.normal * contactPointOffset, transform.rotation);
        }
    }
}
