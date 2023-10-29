using Assets.HomeworksPM.Scripts.MySctripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopUpPlayerLevelView : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _level;
    
    [SerializeField] 
    private TextMeshProUGUI _progressReview;
    
    [SerializeField] 
    private Button _buttonLevelUp;
    
    [SerializeField] 
    private Slider _progress;

    private IPlayerLevelPresentationModel _pm;
    public void Show(IPlayerLevelPresentationModel pm)
    {
        _pm = pm;
        UpdateView();
        _pm.OnModelStateChanged += UpdateView;
    }
    public void Hide()
    {
        _pm.OnModelStateChanged -= UpdateView;
    }
    private void UpdateView()
    {
        _level.text = _pm.GetLevel();
        _progress.maxValue = _pm.GetRequiredExperience();
        _progress.value = _pm.GetProgressValue();
        _progressReview.text = _pm.GetProgressReview();
        UpdateButtonState(_pm.GetButtonState());
    }
    
    private void UpdateButtonState(bool isOn)
    {
        _buttonLevelUp.interactable = isOn;
    }
    
    public UnityEvent GetButtonLevelUpEvent()
    {
        return _buttonLevelUp.onClick;
    }
}
