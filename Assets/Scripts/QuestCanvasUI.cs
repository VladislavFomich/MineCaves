using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCanvasUI : MonoBehaviour
{
    [SerializeField] private Button exitBtn;
    [SerializeField] private QuestItemUI itemUI;
    [SerializeField] private Transform questItemParent;
    [SerializeField] private QuestManager questManager;

    private List<QuestItemUI> _currentQuests = new List<QuestItemUI>();

    private void Awake()
    {
        exitBtn.onClick.AddListener(DisableMenu);
    }

    public void CreateItem(string description,int reward, string id)
    {
        var quest = Instantiate(itemUI, questItemParent);
        quest.discription.text = description;
        quest.reward.text = reward.ToString();
        quest.QuestID = id;
        quest.buttonGetQuest.onClick.AddListener(() => questManager.TakeQuest(id));
        _currentQuests.Add(quest);
    }


    public void TakeQuest(string id)
    {
        foreach (var item in _currentQuests)
        {
            if(item.QuestID == id)
            {
                item.TakeQuest();
                
            }
        }
    }


    public void CompleteQuest(string id)
    {
        foreach (var item in _currentQuests)
        {
            if (item.QuestID == id)
            {
                item.CompleteQuest();
                item.buttonGetReward.onClick.AddListener(() => QuestManager.Instance.GetReward(id,item.reward.transform.position));
                item.buttonGetReward.onClick.AddListener(() => DisableItem(item));
            }
        }
    }


    private void DisableItem(QuestItemUI questItemUI)
    {
        _currentQuests.Remove(questItemUI);
        questItemUI.transform.DOScale(Vector3.zero, 1)
            .OnComplete(() =>
            {
                // По завершению анимации уничтожаем объект
                Destroy(questItemUI.gameObject);
            });
    }


    private void DisableMenu()
    {
        CanvasManager.Instance.CloseCanvas(CanvasManager.CanvasType.Quest);
    }
}

