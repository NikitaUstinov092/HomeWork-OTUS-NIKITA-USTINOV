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
        Container.Bind<ModelViewAdapter>().AsSingle();
        
        Container.BindInterfacesTo<MainPresentationModel>().AsSingle();
        Container.BindInterfacesTo<SetDataManager>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<PopUpFactory>().FromComponentInHierarchy().AsSingle();
        
        Container.Bind<PopUpPlayerLevelView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PopUpUserInfoView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PopUpCharacterInfoView>().FromComponentInHierarchy().AsSingle();
    }
}
