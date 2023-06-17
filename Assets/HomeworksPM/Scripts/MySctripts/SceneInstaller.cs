using Assets.HomeworksPM.Scripts.MySctripts;
using Lessons.Architecture.PM;
using Zenject;
public class SceneInstaller : MonoInstaller<SceneInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CharacterInfo>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<CharacterStat>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerLevel>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<UserInfo>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IPopupPresentationModel>().To<PopUpPresentationModel>().AsSingle();
    }
}
