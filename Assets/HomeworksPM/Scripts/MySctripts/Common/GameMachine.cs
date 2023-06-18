using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameMachine : MonoBehaviour
{
    [Inject]
    private readonly DiContainer _container;

    private void Start()
    {
        foreach(var listner in _container.Resolve<IEnumerable<IInitListner>>())
        {
            listner.OnInit();
        }
    }

    private void OnDisable()
    {
        foreach (var listner in _container.Resolve<IEnumerable<IDisableListner>>())
        {
            listner.Disable();
        }
    }
}
