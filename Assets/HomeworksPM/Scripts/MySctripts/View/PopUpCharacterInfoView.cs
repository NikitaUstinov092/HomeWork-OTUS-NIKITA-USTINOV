using System.Collections.Generic;
using Assets.HomeworksPM.Scripts.MySctripts;
using TMPro;
using UnityEngine;

public class PopUpCharacterInfoView : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI[] statsElements;
    
    private IStatsInfo _pm;
    
    public void Show(IStatsInfo pmStatsInfo)
    {
        _pm = pmStatsInfo;
        UpdateView();
        _pm.OnModelStateChanged += UpdateView;
    }
    public void Hide()
    {
        _pm.OnModelStateChanged -= UpdateView;
    }
    private void UpdateView()
    {
        UpdateStatsElements(_pm.GetStatsReview());
    }
    
    private void UpdateStatsElements(IReadOnlyList<string> statsData)
    {
        for(var i = 0; i < statsElements.Length; i++)         
            statsElements[i].text = statsData[i];          
    }
}
