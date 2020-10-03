using UnityEngine;
using Zenject;

public class DataModelsInstaller : MonoInstaller
{
    [SerializeField] private GameObject _playerFlowerDataModel = default;
    
    public override void InstallBindings()
    {
        Container.Bind<PlayerFlowerDataModel>().FromComponentInNewPrefab(_playerFlowerDataModel).AsSingle();
    }
}
