using Lessons.Architecture.PM;
using UnityEngine;
using Zenject;

public class PopUpFactory : MonoBehaviour, IInitListener, IDisableListener
{
    [SerializeField] 
    private GameObject _prefab;
    
    [SerializeField]
    private Transform _parent;
    
    private DiContainer _container;
    private IDataSender _dataSender;
    private ModelViewAdapter _modelViewAdapter;
    private MainPopUpView _popUp;
   
    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
        _dataSender = _container.Resolve<IDataSender>();
    }
    void IInitListener.OnInit()
    {
        _dataSender = GetComponent<IDataSender>();
        _dataSender.OnDataSend += CreatePopUp;
    }
    void IDisableListener.Disable()
    {
        _dataSender.OnDataSend -= CreatePopUp;
        _modelViewAdapter?.Disable();
    }
    private void CreatePopUp()
    {
        if (_popUp != null)
        {
            _popUp.Show();
            return;
        }
        _popUp = _container.InstantiatePrefabForComponent<MainPopUpView>(_prefab, _parent);
       _modelViewAdapter = new ModelViewAdapter(_popUp.GetComponent<PopUpPlayerLevelView>().GetButtonLevelUpEvent(),_container.Resolve<PlayerLevel>());
    }
}
