using System;
using UnityEngine;
using Zenject;

public class ProjectileEngineManager : MonoBehaviour
{
    public float cislo;
    public GameObject fireExplosion;
    [Inject] private readonly ProjectileEngine.Pool _projectileEnginePool = default;
    [Inject] private readonly PickableSeedManager _pickableSeedManager = default;
    
 
    public void SpawnSeed(Vector3 position, Quaternion rotation, ProjectileType projectileType)
    {
        var seed = _projectileEnginePool.Spawn(position, rotation, projectileType);

        seed.killProjectile += HandleKillProjectile;
    }

    private void HandleKillProjectile(ProjectileEngine projectileEngine, Vector3 collisionHit, Quaternion rotation)
    {
        projectileEngine.killProjectile -= HandleKillProjectile;

        switch (projectileEngine.projectileType)
        {
            case ProjectileType.FireSeed:
                Debug.Log("Fire");
                //GameObject.Instantiate(fireExplosion,collisionHit,rotation);
                break;
            case ProjectileType.PoisonSeed:
                Debug.Log("Poison");
                break;
            case ProjectileType.ElectricSeed:
                Debug.Log("Electric");
                break;
        }
        
        _pickableSeedManager.AddPickableSeed(collisionHit, rotation);
        
        _projectileEnginePool.Despawn(projectileEngine);
    }
}
