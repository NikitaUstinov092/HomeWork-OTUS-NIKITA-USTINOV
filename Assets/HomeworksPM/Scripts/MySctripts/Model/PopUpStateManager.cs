using Assets.HomeworksPM.Scripts.MySctripts;
using UnityEngine;
using Zenject;

public class PopUpStateManager : MonoBehaviour, IInitListner, IDisableListner
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parent;

    private IPopupPresentationModel _pm;
    private IDataSender _dataSender;  

    private GameObject _popUp;

    [Inject]
    private void Construct(IPopupPresentationModel pm, IDataSender dataSender)
    {
        _pm = pm;
        _dataSender = dataSender;
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
        _popUp = Instantiate(_prefab, _parent);
    }

    private void DestroyPopUp()
    {
        Destroy(_popUp);
    }
   
}
