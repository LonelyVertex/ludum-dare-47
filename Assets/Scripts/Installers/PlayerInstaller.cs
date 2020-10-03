using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
     [SerializeField] private AmmunitionStorage _ammunitionStoragePrefab = default;
     
     public override void InstallBindings()
     {
          Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
          Container.Bind<AmmunitionStorage>().FromComponentInNewPrefab(_ammunitionStoragePrefab).AsSingle().NonLazy();
     }
}
