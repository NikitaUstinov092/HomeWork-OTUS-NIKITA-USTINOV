using System;
using UnityEngine;

namespace Assets.HomeworksPM.Scripts.MySctripts
{
    public interface IPopupPresentationModel
    {
        string GetPlayerName();
        string GetLevel();
        string GetDescription();     
        float GetProgress();
        Sprite GetAwatar();
        void OnButtonLevelClicked();
        string[] GetStatsReview();
 
        event Action<bool> OnButtonStateChanged;
        event Action OnButtonClicked;
    }


}