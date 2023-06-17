using Assets.HomeworksPM.Scripts.MySctripts;
using Lessons.Architecture.PM;
using System;
using UnityEngine;
using Zenject;

public class PopUpPresentationModel : IPopupPresentationModel
{
    public event Action OnStateChanged;

    private Lessons.Architecture.PM.CharacterInfo _characterInfo;  
    private PlayerLevel _playerLevel;
    private UserInfo _userInfo;

    private CharacterStat _currentStat;
    private Sprite _currentUserAwatar;
    private string _currentName;
    private string _currentDescription;
    private int _progress;
    private bool _levelUp = false;

    public void Start()
    {
        _characterInfo.OnStatAdded += OnChangeStat;
        _userInfo.OnDescriptionChanged += OnUserDescriptionChanged;
        _userInfo.OnNameChanged += OnUserNameChanged;
        _userInfo.OnIconChanged += OnAwatarChanged;
        _playerLevel.OnExperienceChanged += OnProgressChanged;
        _playerLevel.OnLevelUp += LevelUp;
    }
   
    [Inject]
    private void Construct(Lessons.Architecture.PM.CharacterInfo characterInfo, PlayerLevel playerLevel, UserInfo userInfo)
    {
        _characterInfo = characterInfo;
        _playerLevel = playerLevel;
        _userInfo = userInfo;

        Start(); //убрать 
    }

    private void LevelUp()
    {
        _levelUp = true;
    }

    private void OnProgressChanged(int progress)
    {
        _progress = progress;
    }
    private void OnChangeStat(CharacterStat stat)
    {
        _currentStat = stat;
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


    public bool LevelActive()
    {
        return _levelUp;
    }

    public void OnButtonLevelClicked()
    {
        Debug.Log("Уровень повышен");
    }


    public string[] GetStatsReview()
    {
        var massData = new string[6];
        for(var i = 0; i< massData.Length; i++)
        {
            massData[i] = _currentStat.Name + " " + _currentStat.Value;
        }
        return massData;
    }

    public float GetProgress()
    {
        return _progress;
    }

    public string GetLevel()
    {
        return _playerLevel.CurrentLevel.ToString();
    }
}
