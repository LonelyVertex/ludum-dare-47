using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
     [SerializeField] private GameObject _ammunitionStoragePrefab = default;

     [Space]
     [SerializeField] private GameObject _projectileEnginePrefab = default;
     [SerializeField] private GameObject _pickableSeedPrefab = default;
     public GameObject projectileEngineManager = default;

     [Space]
     [SerializeField] private GameObject _playerSpawnerPrefab = default;
     
    
     public override void InstallBindings()
     {
          Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
          Container.Bind<AmmunitionStorage>().FromComponentInNewPrefab(_ammunitionStoragePrefab).AsSingle().NonLazy();

          Container.Bind<ProjectileEngineManager>().FromComponentInNewPrefab(projectileEngineManager).AsSingle();
          Container.BindMemoryPool<ProjectileEngine, ProjectileEngine.Pool>().FromComponentInNewPrefab(_projectileEnginePrefab).AsCached();

          Container.Bind<PickableSeedManager>().AsSingle();
          Container.BindMemoryPool<PickableSeed, PickableSeed.Pool>().FromComponentInNewPrefab(_pickableSeedPrefab).AsCached();

          Container.Bind<PlayerSpawner>().FromComponentInNewPrefab(_playerSpawnerPrefab).AsSingle();
     }
}
