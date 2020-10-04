using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private GameObject _uiPanelControllerPrefab = default;
    [SerializeField] private GameObject _fadeInOutPrefab = default;
    [SerializeField] private GameObject _gameScenesControllerPrefab = default;
    
    public override void InstallBindings()
    {
        if (_uiPanelControllerPrefab != null)
        {
            Container.Bind<UIPanelController>().FromComponentInNewPrefab(_uiPanelControllerPrefab).AsSingle();
        }
        
        Container.Bind<FadeInOutController>().FromComponentInNewPrefab(_fadeInOutPrefab).AsSingle().NonLazy();
        Container.Bind<GameScenesController>().FromComponentInNewPrefab(_gameScenesControllerPrefab).AsSingle().NonLazy();
    }
}
