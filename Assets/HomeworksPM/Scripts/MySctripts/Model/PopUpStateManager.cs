using Assets.HomeworksPM.Scripts.MySctripts;
using UnityEngine;
using Zenject;

public class PopUpStateManager : MonoBehaviour, IInitListner, IDisableListner
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parent;
    
    private DiContainer _container;
    private IPopupPresentationModel _pm;
    private IDataSender _dataSender;

    private PopUpView _popUp;
   
    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
        _pm = _container.Resolve<IPopupPresentationModel>();
        _dataSender = _container.Resolve<IDataSender>();
    }
    void IInitListner.OnInit()
    {
        _dataSender = GetComponent<IDataSender>();
        _dataSender.OnDataSend += InstatiatePopUp;
        _pm.OnButtonCloseClick += DestroyPopUp;
    }
    void IDisableListner.Disable()
    {
        _dataSender.OnDataSend -= InstatiatePopUp;
        _pm.OnButtonCloseClick -= DestroyPopUp;
    }
    private void InstatiatePopUp()
    {
        _popUp = _container.InstantiatePrefabForComponent<PopUpView>(_prefab, _parent);
    }

    private void DestroyPopUp()
    {
        Destroy(_popUp);
    }
   
}
