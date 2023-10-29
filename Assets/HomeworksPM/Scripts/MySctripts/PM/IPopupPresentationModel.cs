using System;
using UnityEngine;

namespace Assets.HomeworksPM.Scripts.MySctripts
{
    public interface IPopupPresentationModel
    {
        void Start();
        void Stop();
     
        IUserInfoPresentationModel UserInfo { get; }
        
        IPlayerLevelPresentationModel PlayerLevelInfo { get; }
        
        IStatsInfo StatsInfo { get;}
    }

    public interface IStatsInfo
    {
        event Action OnModelStateChanged;
        void Start();
        void Stop();
        string[] GetStatsReview();
    }

    public interface IPlayerLevelPresentationModel
    {
        event Action OnModelStateChanged;
        void Start();
        void Stop();
        string GetLevel();
        int GetRequiredExperience();
        
        int GetProgressValue();    
        string GetProgressReview();
        bool GetButtonState();
    }

    public interface IUserInfoPresentationModel
    {
        event Action OnModelStateChanged;
        void Start();
        void Stop();
        string GetPlayerName();
        string GetDescription();
        Sprite GetAvatar();
    }
}