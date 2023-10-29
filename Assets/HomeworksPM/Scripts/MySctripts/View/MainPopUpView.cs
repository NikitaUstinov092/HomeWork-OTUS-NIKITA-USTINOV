using Assets.HomeworksPM.Scripts.MySctripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainPopUpView : MonoBehaviour
 {
     [SerializeField] 
     private Button _closePopUp;
     
     private IPopupPresentationModel _pm;
        
     private PopUpPlayerLevelView _popUpPlayerLevelView;
     private PopUpUserInfoView _popUpUserInfoView;
     private PopUpCharacterInfoView _popUpCharacterInfoView;
        
     [Inject]
     private void Construct(IPopupPresentationModel pm, PopUpPlayerLevelView popUpPlayerLevelView,
            PopUpUserInfoView popUpUserInfoView, PopUpCharacterInfoView popUpCharacterInfoView)
     {
         _pm = pm;
         _popUpPlayerLevelView = popUpPlayerLevelView;
         _popUpUserInfoView = popUpUserInfoView;
         _popUpCharacterInfoView = popUpCharacterInfoView;
     }
     public void Show()
     {
         gameObject.SetActive(true);
         _pm.Start();
         ShowPopUps();
         AddButtonsListeners();
     }
     public void Hide()
     {
         gameObject.SetActive(false);
         _pm.Stop();
         HidePopUps();
         RemoveButtonsListeners();
     }
     private void ShowPopUps()
     {
         _popUpPlayerLevelView.Show(_pm.PlayerLevelInfo);
         _popUpUserInfoView.Show(_pm.UserInfo);
         _popUpCharacterInfoView.Show(_pm.StatsInfo);
     }
     private void HidePopUps()
     {
         _popUpPlayerLevelView.Hide();
         _popUpUserInfoView.Hide();
         _popUpCharacterInfoView.Hide();
     }
     private void OnEnable()
     {
         Show();
     }
     private void OnDisable()
     {
         Hide();
     }
     private void AddButtonsListeners()
     {
         _closePopUp.onClick.AddListener(Hide);
     }
     private void RemoveButtonsListeners()
     {
         _closePopUp.onClick.RemoveListener(Hide);
     }
 }
