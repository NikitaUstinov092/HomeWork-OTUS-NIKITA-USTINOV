using System;
using UnityEngine;

namespace Assets.HomeworksPM.Scripts.MySctripts
{
    public interface IPopupPresentationModel
    {
        event Action OnModelStateChanged;
        event Action OnButtonLevelUpClick;
        event Action OnButtonCloseClick;
        string GetPlayerName();
        string GetLevel();
        string GetDescription();
        string GetProgressReview();
        string[] GetStatsReview();
        int GetRequiredExperience();
        int GetProgressValue();      
        Sprite GetAvatar();
        void OnButtonLevelUpClicked();
        void OnButtonCloseClicked();
        bool GetButtonState();
    }


}