using System;
using UnityEngine;
using Zenject;

public class ProjectileEngine : MonoBehaviour
{
    public Rigidbody2D _rigidbody2D;
    public float projectileSpeed;
    public float lifespan;
    public PlayerFlowerType playerFlowerType;
    public FlowerGraphicSwapper _flowerGraphicSwapper = default;
    
    public event Action<ProjectileEngine, GameObject, Vector3, Quaternion, Vector3> killProjectile;

    private float _currentLifeSpan;
    private PlayerFlowerType _playerFlowerType;
    
    public class Pool : MonoMemoryPool<Vector3, Quaternion, PlayerFlowerType, ProjectileEngine>
    {
        protected override void Reinitialize(Vector3 position, Quaternion rotation, PlayerFlowerType playerFlowerType, ProjectileEngine projectileEngine)
        {
            projectileEngine.Reinitialize(position, rotation, playerFlowerType);
        }
    }

    private void Reinitialize(Vector3 position, Quaternion rotation, PlayerFlowerType type)
    {
        _rigidbody2D.position = position;
        _rigidbody2D.rotation = rotation.eulerAngles.z;

        _currentLifeSpan = lifespan;
        playerFlowerType = type;
        _flowerGraphicSwapper.SwapGraphics(type);
    }
    
    private void Update()
    { 
        _currentLifeSpan -= Time.deltaTime;
        if (_currentLifeSpan <= 0)
        {
            killProjectile?.Invoke(this, null, Vector3.zero, Quaternion.identity, Vector3.zero);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = transform.right * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var contact = other.GetContact(0);
     
        killProjectile?.Invoke(this, other.gameObject, contact.point, transform.rotation, contact.normal);
    }
}
