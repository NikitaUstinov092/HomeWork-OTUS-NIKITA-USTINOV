using System;
using UnityEngine;

namespace Assets.HomeworksPM.Scripts.MySctripts
{
    public interface IPopupPresentationModel
    {
        event Action OnButtonLevelUpClick;
        event Action OnButtonCloseClick;
        string GetPlayerName();
        string GetLevel();
        string GetDescription();
        string GetProgressRewiew();
        string[] GetStatsReview();
        int GetRequiredExperience();
        int GetProgressValue();      
        Sprite GetAwatar();
        void OnButtonLevelUpClicked();
        void OnButtonCloseClicked();
        bool GetButtonState();
    }


}