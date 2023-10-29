using Lessons.Architecture.PM;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

public class SetDataManager : MonoBehaviour, IInitListener, IDataSender
{
    public event Action OnDataSend;

    [SerializeField] 
    private StatData[] _stats;
    
    [SerializeField] 
    private string _name;
    
    [SerializeField] 
    private string _description;
    
    [SerializeField] 
    private Sprite _icon;
    
    [SerializeField] 
    private int _expirience;

    private Lessons.Architecture.PM.CharacterInfo _characterInfo;
    private PlayerLevel _playerLevel;
    private UserInfo _userInfo;
  
    private const int MaxStatCount = 6; 
    void IInitListener.OnInit()
    {
        _stats = new StatData[MaxStatCount];     
    }
  
    [Inject]
    private void Construct(Lessons.Architecture.PM.CharacterInfo characterInfo, PlayerLevel playerLevel, UserInfo userInfo)
    {
        _characterInfo = characterInfo; 
        _playerLevel = playerLevel;
        _userInfo = userInfo;
    }
    private void OnValidate()
    {
        if(_stats.Length > MaxStatCount) 
            Array.Resize(ref _stats, MaxStatCount);        
    }
    private void Submit()
    {
        _userInfo.ChangeName(_name);
        _userInfo.ChangeDescription(_description);
        _userInfo.ChangeIcon(_icon);
        _playerLevel.AddExperience(_expirience);

        _characterInfo.ClearStat();
        foreach (var t in _stats)
        {
            var stat = new CharacterStat(t.Name, t.Value);
            _characterInfo.AddStat(stat);
        }
    }

    [Button]
    public void SubmitData()
    {
        if (!CheckDataNotEmpty())
            return;
        Submit();
        OnDataSend?.Invoke();
    }
    private bool CheckDataNotEmpty()
    {
        if(_name == string.Empty)
        {
            Debug.LogError("Имя не заполнено");
            return false;
        }
        if (_description == string.Empty)
        {
            Debug.LogError("Описание не заполнено");
            return false;
        }
        if (_icon == null)
        {
            Debug.LogError("Иконка не заполнена");
            return false;
        }
        var index = 0;
        for (; index < _stats.Length; index++)
        {
            var data = _stats[index];
            if (data != null && data.Name != string.Empty) continue;
            Debug.LogError("Некоторые данные статистики не заполнены");
            return false;
        }
        return true;
    }
}


[System.Serializable]
public class StatData
{
    public string Name;
    public int Value;
}



