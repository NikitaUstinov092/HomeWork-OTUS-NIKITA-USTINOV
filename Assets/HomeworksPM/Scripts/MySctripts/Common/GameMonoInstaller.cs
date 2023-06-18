using UnityEngine;
using Zenject;

public class GameMonoInstaller : MonoInstaller<GameMonoInstaller>
{
    [SerializeField] private MonoBehaviour[] _allIListnersMono;
    public override void InstallBindings()
    {
        for (var i = 0; i < _allIListnersMono.Length; i++)
        {
            var massComp = _allIListnersMono[i].GetComponents<MonoBehaviour>();

            for (var j = 0; j < massComp.Length; j++)
            {
                if (massComp[j] is IGameListener marcker)
                    Container.BindInterfacesTo(marcker.GetType()).FromInstance(marcker).AsCached(); 

            }


        }
    }

}
