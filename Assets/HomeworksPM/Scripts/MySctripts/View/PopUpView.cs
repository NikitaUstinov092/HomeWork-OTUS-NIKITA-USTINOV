using Lessons.Architecture.PM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.HomeworksPM.Scripts.MySctripts
{
    public sealed class PopUpView : MonoBehaviour
    {
        #region UI elements

        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _progressReview;

        [SerializeField] private Image _awatar;
        [SerializeField] private Slider _progress;
        [SerializeField] private Button _levelUp;
        [SerializeField] private Button _closePopUp;

        [SerializeField] private TextMeshProUGUI[] statsElements;

        #endregion

        private IPopupPresentationModel _pm;

        private void OnEnable()
        {        
            UpdateView();
            AddListners();
        }
        private void OnDestroy()
        {
            RemoveListners();
        }

        [Inject]
        private void Construct(IPopupPresentationModel pm)
        {
            _pm = pm;           
        }

        public void UpdateView()
        {
            _name.text = _pm.GetPlayerName();
            _level.text = _pm.GetLevel();
            _description.text = _pm.GetDescription();
            _awatar.sprite = _pm.GetAwatar();
           
            _progress.maxValue = _pm.GetRequiredExperience();
            _progress.value = _pm.GetProgressValue();
            _progressReview.text = _pm.GetProgressRewiew();

            OnButtonStateChanged(_pm.GetButtonState());
            UpdateStatsElements(_pm.GetStatsReview());
        }

        private void AddListners()
        {
            _levelUp.onClick.AddListener(() => OnButtonClicked());
            _levelUp.onClick.AddListener(() => DestroyPopUp());
            _closePopUp.onClick.AddListener(() => DestroyPopUp());
        }

        private void RemoveListners()
        {
            _levelUp.onClick.RemoveListener(() => OnButtonClicked());
            _levelUp.onClick.RemoveListener(() => DestroyPopUp());
            _closePopUp.onClick.RemoveListener(() => DestroyPopUp());
        }


        private void UpdateStatsElements(string [] statsData)
        {
            for(var i = 0; i < statsElements.Length; i++)         
                statsElements[i].text = statsData[i];          
        }

        private void OnButtonStateChanged(bool isOn)
        {
            _levelUp.interactable = isOn;
        }

        private void OnButtonClicked()
        {
            _pm.OnButtonLevelUpClicked();
        }

        private void DestroyPopUp()
        {
            Destroy(gameObject);
        }

    }
}