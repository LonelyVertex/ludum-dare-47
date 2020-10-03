using Zenject;

public class PlayerControllerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerInputState>().AsSingle();
    }
}
