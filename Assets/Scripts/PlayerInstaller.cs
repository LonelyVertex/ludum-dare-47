using Zenject;

public class PlayerInstaller : MonoInstaller
{
     public override void InstallBindings()
     {
          Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
     }
}
