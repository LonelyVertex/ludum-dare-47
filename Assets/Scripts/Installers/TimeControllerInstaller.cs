using UnityEngine;
using Zenject;

public class TimeControllerInstaller : MonoInstaller
{
    public GameObject timeControllerPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<TimeController>().FromComponentInNewPrefab(timeControllerPrefab).AsSingle();
    }
}