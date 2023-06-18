using Lessons.Architecture.PM;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

public class SetDataManager : MonoBehaviour, IInitListner, IDataSender
{
    [SerializeField] private StatData[] _stats;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _expirience;

    private Lessons.Architecture.PM.CharacterInfo _characterInfo;
    private PlayerLevel _playerLevel;
    private UserInfo _userInfo;
       
    private const int _maxStatCount = 6;

    public event Action OnDataSend;

    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        _stats = new StatData[_maxStatCount];
    }

    [Inject]
    private void Constrcut(Lessons.Architecture.PM.CharacterInfo characterInfo, PlayerLevel playerLevel, UserInfo userInfo)
    {
        _characterInfo = characterInfo; 
        _playerLevel = playerLevel;
        _userInfo = userInfo;   
    }

    private void OnValidate()
    {
        if(_stats.Length > _maxStatCount) 
            Array.Resize(ref _stats, _maxStatCount);        
    }


    private void Submit()
    {
        _userInfo.ChangeName(_name);
        _userInfo.ChangeDescription(_description);
        _userInfo.ChangeIcon(_icon);
        _playerLevel.AddExperience(_expirience);

        for(var i = 0; i < _stats.Length; i++)
        {
            var stat = new CharacterStat(_stats[i].Name, _stats[i].Value);
            _characterInfo.AddStat(stat);
        }
    }

    [Button]
    public void OpenPopUp()
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

        foreach (var data in _stats) 
        {
            if (data == null || data.Name == string.Empty) 
            {
                Debug.LogError("Некоторые данные статистики не заполнены");
                return false;
            }
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



