using Lessons.Architecture.PM;
using Zenject;
public class SceneInstaller : MonoInstaller<SceneInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<CharacterInfo>().AsSingle();
        Container.Bind<PlayerLevel>().AsSingle();
        Container.Bind<UserInfo>().AsSingle();
       
        Container.BindInterfacesTo<PopUpPresentationModel>().AsSingle();
        Container.BindInterfacesTo<SetDataManager>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<PopUpStateManager>().FromComponentInHierarchy().AsSingle();
    }
}
