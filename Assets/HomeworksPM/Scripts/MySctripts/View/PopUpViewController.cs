using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.HomeworksPM.Scripts.MySctripts
{
    public sealed class PopUpViewController : MonoBehaviour
    {
        #region UI elements
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private TextMeshProUGUI _description;
        
        [SerializeField] private Image _awatar;
        [SerializeField] private Slider _progress;
        [SerializeField] private Button _levelUp;

        [SerializeField] private TextMeshProUGUI[] statsElements;
        
        #endregion

        private IPopupPresentationModel _pm;

        private void Start()
        {
            Show();
        }

        [Inject]
        private void Construct(IPopupPresentationModel pm)
        {
            _pm = pm;
        }

        private void Show()
        {
            _name.text = _pm.GetPlayerName();
            _level.text = _pm.GetLevel();
            _description.text = _pm.GetDescription();
            _awatar.sprite = _pm.GetAwatar();
            _progress.value = _pm.GetProgress();

            UpdateStatsElements(_pm.GetStatsReview());


            _pm.OnStateChanged += OnButtonStateChanged;
        }


        private void UpdateStatsElements(string [] statsData)
        {
            for(var i = 0; i < statsElements.Length; i++)
            {
                statsElements[i].text = statsData[i];
            }
        }

        private void OnButtonStateChanged()
        {
            _levelUp.interactable =_pm.LevelActive();
        }

        private void OnButtonClicked()
        {
            _pm.OnButtonLevelClicked();
        }





    }
}