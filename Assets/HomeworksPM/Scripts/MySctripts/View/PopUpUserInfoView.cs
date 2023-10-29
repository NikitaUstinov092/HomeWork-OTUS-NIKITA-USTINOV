using Assets.HomeworksPM.Scripts.MySctripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUserInfoView : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _name;
        
    [SerializeField] 
    private TextMeshProUGUI _description;
        
    [SerializeField] 
    private Image _avatar;
    
    private IUserInfoPresentationModel _pm;
    public void Show(IUserInfoPresentationModel pmUserInfo)
    {
        _pm = pmUserInfo;
        UpdateView();
        _pm.OnModelStateChanged += UpdateView;
    }
    public void Hide()
    {
        _pm.OnModelStateChanged -= UpdateView;
    }
    private void UpdateView()
    {
        _name.text = _pm.GetPlayerName();
        _description.text = _pm.GetDescription();
        _avatar.sprite = _pm.GetAvatar();
    }
}
