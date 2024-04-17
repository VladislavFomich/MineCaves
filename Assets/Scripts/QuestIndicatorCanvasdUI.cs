using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestIndicatorCanvasdUI : MonoBehaviour
{
    [SerializeField] private QuestIndicatorUI indicatorUI;
    [SerializeField] private Transform itemParent;
    [SerializeField] private Button exitBtn;
    private List<QuestIndicatorUI> _currentQuests = new List<QuestIndicatorUI>();

    private void Awake()
    {
        exitBtn.onClick.AddListener(() => CanvasManager.Instance.CloseCanvas(CanvasManager.CanvasType.QuestIndicator));
    }

    public void CreateItem(string questName,string discription,float maxProgress,float currentProgress)
    {
        var item = Instantiate(indicatorUI, itemParent);
        item.Description.text = discription;
        item.ProgressText.text = $"{currentProgress.ToString()}/{maxProgress.ToString()}";
        item.Slider.maxValue = maxProgress;
        item.Slider.value = currentProgress;
        item.QuestName = questName;
        _currentQuests.Add(item);
    }

    public void UpdateProgress(string questName,float progress) 
    {
        foreach(var item in _currentQuests)
        {
            if(item.QuestName == questName)
            {
                item.Slider.value = progress;
                item.ProgressText.text = $"{item.Slider.value.ToString()}/{item.Slider.maxValue.ToString()}";
            }
        }
    }

    public void DestroyItem(string questName)
    {
        List<QuestIndicatorUI> list = new List<QuestIndicatorUI> ();
        foreach (var item in _currentQuests)
        {
            if (item.QuestName == questName)
            {
                list.Add(item);
            }
        }


        foreach (var item in list)
        {
            _currentQuests.Remove(item);
            Destroy(item.gameObject);
        }
    }
}
