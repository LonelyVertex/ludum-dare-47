using UnityEngine;
using Zenject;

public class ProjectileEngineManager : MonoBehaviour
{
    [Inject] private readonly ProjectileEngine.Pool _projectileEnginePool = default;
    [Inject] private readonly PickableSeedManager _pickableSeedManager = default;

    public GameObject fireExplosion;
    public GameObject electricExplosion;
    public GameObject poisonExplosion;
    private GameObject tmp;
    public GameObject PiercingExplosion;
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
                GameObject.Instantiate(poisonExplosion, hitPosition, rotation);
                break;
            case PlayerFlowerType.Electric:
                tmp = GameObject.Instantiate(electricExplosion, hitPosition, rotation);
                tmp.GetComponent<ElectricSeedExplosion>().FirstTarget = hitGameObject;
                break;
            case PlayerFlowerType.Piercing:
                tmp = GameObject.Instantiate(PiercingExplosion, hitPosition, rotation);
                tmp.GetComponent<PiercingSeedExplosion>().FirstTarget = hitGameObject;
                break;
        }

        // Spawn ammunition to collect
        _pickableSeedManager.AddPickableSeed(hitPosition, rotation, hitNormal);
    }
}
