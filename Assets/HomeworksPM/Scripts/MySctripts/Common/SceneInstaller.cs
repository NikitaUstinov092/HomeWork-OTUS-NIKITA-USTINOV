using Assets.HomeworksPM.Scripts.MySctripts;
using Lessons.Architecture.PM;
using Zenject;
public class SceneInstaller : MonoInstaller<SceneInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<CharacterInfo>().AsSingle();
        Container.Bind<PlayerLevel>().AsSingle();
        Container.Bind<UserInfo>().AsSingle();
        Container.Bind<IPopupPresentationModel>().To<PopUpPresentationModel>().AsSingle();
    }
}
