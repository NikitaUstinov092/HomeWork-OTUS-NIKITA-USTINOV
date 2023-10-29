using Assets.HomeworksPM.Scripts.MySctripts;
using Lessons.Architecture.PM;
using System;
using UnityEngine;
using Zenject;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

public class MainPresentationModel : IPopupPresentationModel, IInitListener
{
    IUserInfoPresentationModel IPopupPresentationModel.UserInfo => _userInfoPm;
    IPlayerLevelPresentationModel IPopupPresentationModel.PlayerLevelInfo => _playerLevelInfo;
    IStatsInfo IPopupPresentationModel.StatsInfo => _statsInfo;
   
    private PlayerLevel _playerLevel;
    private UserInfo _userInfo;
    private CharacterInfo _characterInfo;  
   
    private IUserInfoPresentationModel _userInfoPm;
    private IPlayerLevelPresentationModel _playerLevelInfo;
    private IStatsInfo _statsInfo;
    
    void IInitListener.OnInit()
    {
        _userInfoPm = new UserInfoPresentationModel(_userInfo);
        _playerLevelInfo = new PlayerLevelInfoPresentationModel(_playerLevel);
        _statsInfo = new StatsInfoPresentationModel(_characterInfo);
    }

    [Inject]
    private void Construct(CharacterInfo characterInfo, PlayerLevel playerLevel, UserInfo userInfo)
    {
        _characterInfo = characterInfo;
        _playerLevel = playerLevel;
        _userInfo = userInfo;
    }
    public void Start()
    {
        _userInfoPm.Start();
        _playerLevelInfo.Start();
        _statsInfo.Start();
    }
    public void Stop()
    {
        _userInfoPm.Stop();
        _playerLevelInfo.Stop();
        _statsInfo.Stop();
    }
}

public class UserInfoPresentationModel: IUserInfoPresentationModel
{
    public event Action OnModelStateChanged;
    
    private readonly UserInfo _userInfo;
    public UserInfoPresentationModel(UserInfo userInfo)
    {
        _userInfo = userInfo;
    }
    public void Start()
    {
        _userInfo.OnDescriptionChanged += OnUserDescriptionChanged;
        _userInfo.OnNameChanged += OnUserNameChanged;
        _userInfo.OnIconChanged += OnAvatarChanged;
    }
    public void Stop()
    {
        _userInfo.OnDescriptionChanged -= OnUserDescriptionChanged;
        _userInfo.OnNameChanged -= OnUserNameChanged;
        _userInfo.OnIconChanged -= OnAvatarChanged;
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
}

public class PlayerLevelInfoPresentationModel: IPlayerLevelPresentationModel
{
    public event Action OnModelStateChanged;
    
    private readonly PlayerLevel _playerLevel;
    public PlayerLevelInfoPresentationModel(PlayerLevel playerLevel)
    {
        _playerLevel = playerLevel;
    }
    public void Start()
    {
        _playerLevel.OnExperienceChanged += OnProgressChanged;
        _playerLevel.OnLevelUp += OnProgressChanged;
    }
    public void Stop()
    {
        _playerLevel.OnExperienceChanged -= OnProgressChanged;
        _playerLevel.OnLevelUp -= OnProgressChanged;
    }
    private void OnProgressChanged(int value)
    {
        OnModelStateChanged?.Invoke();
    }
    private void OnProgressChanged()
    {
        OnModelStateChanged?.Invoke();
    }
    
    public int GetProgressValue()
    {
        return _playerLevel.CurrentExperience;
    }
    public string GetProgressReview()
    {
        return $"XP: {_playerLevel.CurrentExperience} / {GetRequiredExperience()}"; 
    }
    public int GetRequiredExperience()
    {
        return _playerLevel.RequiredExperience;
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
public class StatsInfoPresentationModel : IStatsInfo
{
    public event Action OnModelStateChanged;
    
    private readonly CharacterInfo _characterInfo;
    public StatsInfoPresentationModel(CharacterInfo characterInfo)
    {
        _characterInfo = characterInfo;
    }
    public void Start()
    {
        _characterInfo.OnStatAdded += OnUserStatesChanged;
        _characterInfo.OnStatRemoved += OnUserStatesChanged;
    }
    public void Stop()
    {
        _characterInfo.OnStatAdded -= OnUserStatesChanged;
        _characterInfo.OnStatRemoved -= OnUserStatesChanged;
    }
    private void OnUserStatesChanged(CharacterStat stat)
    {
        OnModelStateChanged?.Invoke();
    }
    public string[] GetStatsReview()
    {
        var massData = new string[6];
        var stats = _characterInfo.GetStats();

        for (var i = 0; i< massData.Length; i++)
        {
            if (i >= stats.Length)
                break;
            massData[i] =  $"{stats[i].Name} {stats[i].Value}";
        }
        return massData;
    }
}
