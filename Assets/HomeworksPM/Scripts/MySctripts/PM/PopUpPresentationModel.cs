using Assets.HomeworksPM.Scripts.MySctripts;
using Lessons.Architecture.PM;
using System;
using UnityEngine;
using Zenject;

public class PopUpPresentationModel : IPopupPresentationModel, IInitListner, IDisableListner
{
    public event Action OnModelStateChanged;
    public event Action OnButtonLevelUpClick;
    public event Action OnButtonCloseClick;

    private Lessons.Architecture.PM.CharacterInfo _characterInfo;  
    private PlayerLevel _playerLevel;
    private UserInfo _userInfo;
    
    void IInitListner.OnInit()
    {
        _userInfo.OnDescriptionChanged += OnUserDescriptionChanged;
        _userInfo.OnNameChanged += OnUserNameChanged;
        _userInfo.OnIconChanged += OnAvatarChanged;
        _playerLevel.OnExperienceChanged += OnProgressChanged;

        OnButtonLevelUpClick += _playerLevel.LevelUp;
    }
    void IDisableListner.Disable()
    {
        _userInfo.OnDescriptionChanged -= OnUserDescriptionChanged;
        _userInfo.OnNameChanged -= OnUserNameChanged;
        _userInfo.OnIconChanged -= OnAvatarChanged;
        _playerLevel.OnExperienceChanged -= OnProgressChanged;

        OnButtonLevelUpClick -= _playerLevel.LevelUp;
    }

    [Inject]
    private void Construct(Lessons.Architecture.PM.CharacterInfo characterInfo, PlayerLevel playerLevel, UserInfo userInfo)
    {
        _characterInfo = characterInfo;
        _playerLevel = playerLevel;
        _userInfo = userInfo;
    }
    private void OnProgressChanged(int value)
    {
        OnModelStateChanged?.Invoke();
    }
    private void OnAvatarChanged(Sprite avatar)
    {
        OnModelStateChanged?.Invoke();
    }
    private void OnUserNameChanged(string name)
    {
        OnModelStateChanged?.Invoke();
    }
    private void OnUserDescriptionChanged(string description)
    {
        OnModelStateChanged?.Invoke();
    }
    public Sprite GetAvatar()
    {
        return _userInfo.Icon;
    }
    public string GetDescription()
    {
        return _userInfo.Description;
    }
    public string GetPlayerName()
    {
        return _userInfo.Name;
    }
    public void OnButtonLevelUpClicked()
    {
        _playerLevel.LevelUp();
    }
    public void OnButtonCloseClicked()
    {
        OnButtonCloseClick?.Invoke();
    }
    public string[] GetStatsReview()
    {
        var massData = new string[6];
        CharacterStat[] stats = _characterInfo.GetStats();

        for (var i = 0; i< massData.Length; i++)
        {
            if (i >= stats.Length)
                break;
            massData[i] = stats[i].Name + " " + stats[i].Value;
        }
        return massData;
    }
    public int GetRequiredExperience()
    {
        return _playerLevel.RequiredExperience;
    }
    public int GetProgressValue()
    {
        return _playerLevel.CurrentExperience;
    }

    public string GetProgressReview()
    {
        return $"XP: {_playerLevel.CurrentExperience} / {GetRequiredExperience()}"; 
    }

    public string GetLevel()
    {
        return $"Level: {_playerLevel.CurrentLevel}";
    }

    public bool GetButtonState()
    {
        return _playerLevel.CanLevelUp();
    }
}
