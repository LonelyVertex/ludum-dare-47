using UnityEngine;
using Zenject;

public class ProjectileEngineManager
{
    [Inject] private readonly ProjectileEngine.Pool _projectileEnginePool = default;
    [Inject] private readonly PickableSeedManager _pickableSeedManager = default;
    
    public void SpawnSeed(Vector3 position, Quaternion rotation)
    {
        var seed = _projectileEnginePool.Spawn(position, rotation);

        seed.killProjectile += HandleKillProjectile;
    }

    private void HandleKillProjectile(ProjectileEngine projectileEngine, Vector3 collisionHit, Quaternion rotation)
    {
        projectileEngine.killProjectile -= HandleKillProjectile;
        
        _pickableSeedManager.AddPickableSeed(collisionHit, rotation);
        
        _projectileEnginePool.Despawn(projectileEngine);
    }
}
