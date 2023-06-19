using Assets.HomeworksPM.Scripts.MySctripts;
using Lessons.Architecture.PM;
using System;
using UnityEngine;
using Zenject;

public class PopUpPresentationModel : IPopupPresentationModel, IInitListner, IDisableListner
{
    public event Action OnButtonLevelUpClick;
    public event Action OnButtonCloseClick;

    private Lessons.Architecture.PM.CharacterInfo _characterInfo;  
    private PlayerLevel _playerLevel;
    private UserInfo _userInfo;

    private Sprite _currentUserAwatar;
    private string _currentName;
    private string _currentDescription;
    private int _progress;
    private bool _buttonInteractive;


    void IInitListner.OnInit()
    {
        _userInfo.OnDescriptionChanged += OnUserDescriptionChanged;
        _userInfo.OnNameChanged += OnUserNameChanged;
        _userInfo.OnIconChanged += OnAwatarChanged;
        _playerLevel.OnExperienceChanged += OnProgressChanged;

        OnButtonLevelUpClick += _playerLevel.LevelUp;
    }
    void IDisableListner.Disable()
    {
        _userInfo.OnDescriptionChanged -= OnUserDescriptionChanged;
        _userInfo.OnNameChanged -= OnUserNameChanged;
        _userInfo.OnIconChanged -= OnAwatarChanged;
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

    private void SetButtonState()
    {
        if (_playerLevel.CanLevelUp())
            _buttonInteractive = true;
        else _buttonInteractive = false;
    }

    private void OnProgressChanged(int progress)
    {
        _progress = progress;
        SetButtonState();
    }
    

    private void OnAwatarChanged(Sprite awatar)
    {
        _currentUserAwatar = awatar;
    }

    private void OnUserNameChanged(string name)
    {
        _currentName = name;
    }

    private void OnUserDescriptionChanged(string description)
    {
        _currentDescription = description;
    }


    public Sprite GetAwatar()
    {
        return _currentUserAwatar;
    }

   
    public string GetDescription()
    {
        return _currentDescription;
    }

    public string GetPlayerName()
    {
        return _currentName;
    }

    public void OnButtonLevelUpClicked()
    {
        OnButtonLevelUpClick?.Invoke();        
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
        return _progress;
    }

    public string GetProgressRewiew()
    {
        return "XP: " + _progress + " / " + GetRequiredExperience().ToString(); 
    }

    public string GetLevel()
    {
        return "Level: " + _playerLevel.CurrentLevel.ToString();
    }

    public bool GetButtonState()
    {
        return _buttonInteractive;
    }

}
