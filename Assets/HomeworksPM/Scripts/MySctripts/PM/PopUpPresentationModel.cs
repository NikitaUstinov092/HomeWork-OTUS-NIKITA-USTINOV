using Assets.HomeworksPM.Scripts.MySctripts;
using Lessons.Architecture.PM;
using System;
using UnityEngine;
using Zenject;

public class PopUpPresentationModel : IPopupPresentationModel
{
    public event Action<bool> OnButtonStateChanged;
    public event Action OnButtonClicked;

    private Lessons.Architecture.PM.CharacterInfo _characterInfo;  
    private PlayerLevel _playerLevel;
    private UserInfo _userInfo;

    private Sprite _currentUserAwatar;
    private string _currentName;
    private string _currentDescription;
    private int _progress;

    public void Start()
    {
        _userInfo.OnDescriptionChanged += OnUserDescriptionChanged;
        _userInfo.OnNameChanged += OnUserNameChanged;
        _userInfo.OnIconChanged += OnAwatarChanged;
        _playerLevel.OnExperienceChanged += OnProgressChanged;
        _playerLevel.OnLevelUp += OnLevelUp;
    }
   
    [Inject]
    private void Construct(Lessons.Architecture.PM.CharacterInfo characterInfo, PlayerLevel playerLevel, UserInfo userInfo)
    {
        _characterInfo = characterInfo;
        _playerLevel = playerLevel;
        _userInfo = userInfo;

        Start(); //убрать 
    }

    private void OnLevelUp()
    {
        OnButtonStateChanged?.Invoke(true);
    }

    private void OnProgressChanged(int progress)
    {
        _progress = progress;
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

    public void OnButtonLevelClicked()
    {
        OnButtonClicked?.Invoke();
        Debug.Log("Кнопка уровень повышен нажата");
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

    public float GetProgress()
    {
        return _progress;
    }

    public string GetLevel()
    {
        return "Level: " + _playerLevel.CurrentLevel.ToString();
    }
}
