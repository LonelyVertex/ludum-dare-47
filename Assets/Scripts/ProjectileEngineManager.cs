using UnityEngine;
using Zenject;

public class ProjectileEngineManager : MonoBehaviour
{
    [Inject] private readonly ProjectileEngine.Pool _projectileEnginePool = default;
    [Inject] private readonly PickableSeedManager _pickableSeedManager = default;
    
    public GameObject fireExplosion;
 
    public void SpawnSeed(Vector3 position, Quaternion rotation, PlayerFlowerType playerFlowerType)
    {
        var seed = _projectileEnginePool.Spawn(position, rotation, playerFlowerType);

        seed.killProjectile += HandleKillProjectile;
    }

    private void HandleKillProjectile(ProjectileEngine projectileEngine, GameObject hitGameObject, Vector3 hitPosition, Quaternion rotation, Vector3 hitNormal)
    {
        projectileEngine.killProjectile -= HandleKillProjectile;

        _projectileEnginePool.Despawn(projectileEngine);
        
        // Spawn different effect
        switch (projectileEngine.playerFlowerType)
        {
            case PlayerFlowerType.Fire:
               
                GameObject.Instantiate(fireExplosion, hitPosition, rotation);
                break;
            case PlayerFlowerType.Poison:
                Debug.Log("Poison");
                break;
            case PlayerFlowerType.Electric:
                Debug.Log("Electric");
                break;
            case PlayerFlowerType.Piercing:
                Debug.Log("Piercing");
                break;
        }
        
        // Spawn ammunition to collect
        _pickableSeedManager.AddPickableSeed(hitPosition, rotation, hitNormal);
    }
}
