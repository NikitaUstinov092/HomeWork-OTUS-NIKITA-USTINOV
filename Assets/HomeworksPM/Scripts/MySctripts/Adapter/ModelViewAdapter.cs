using Lessons.Architecture.PM;
using UnityEngine.Events;

public class ModelViewAdapter
{
    private PlayerLevel _playerLevel;
    private UnityEvent OnButtonClickedEvent;
    
    public ModelViewAdapter(UnityEvent levelUpEvent, PlayerLevel playerLevel)
    {
        OnButtonClickedEvent = levelUpEvent;
        _playerLevel = playerLevel;
        Enable();
    }
    private void Enable()
    {
        OnButtonClickedEvent.AddListener(() => _playerLevel.LevelUp());
    }
    public void Disable()
    {
        OnButtonClickedEvent?.RemoveAllListeners();
    }
}
