using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using QuantumTek.QuantumQuest;


[RequireComponent(typeof(QQ_QuestHandler))]
public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private QuestIndicatorCanvasdUI questIndicatorCanvasdUI;
    [SerializeField] private QuestCanvasUI questCanvasUI;
    private QQ_QuestHandler handler;


    private const string saveKey = "questSave";

    private void Awake()
    {
        handler = GetComponent<QQ_QuestHandler>();
        Load();
    }


    public void TakeQuest(string questName)
    {
        handler.ActivateQuest(questName);
        questCanvasUI.TakeQuest(questName);
        var quest = handler.GetQuest(questName);
        questIndicatorCanvasdUI.CreateItem(questName,quest.Description, quest.Tasks[0].MaxProgress, quest.Tasks[0].Progress);
    }


    public void CompliteQuest(int questIndex)
    {

    }


    public void RewardedQuest(int questIndex)
    {

    }

    public void TaskProgress(string task, float amount)
    {
        foreach (var item in handler.Quests)
        {
            if(item.Value.Status == QQ_QuestStatus.Active && item.Value.Tasks[0].Name == task)
            {
                handler.ProgressTask(item.Key,task ,amount);
                questIndicatorCanvasdUI.UpdateProgress(item.Key, item.Value.Tasks[0].Progress);


                if (item.Value.Tasks[0].Completed)
                {
                    questCanvasUI.CompleteQuest(item.Key);
                };
            }
        }
       
    }


    public void GetReward(string quest,Vector3 buttonPostion)
    {
        foreach (var item in handler.Quests)
        {
            if (item.Value.Status == QQ_QuestStatus.Active && item.Key == quest)
            {
                handler.GetQuest(quest).Tasks[1].Complete();
                handler.GetQuest(quest).CompleteQuest();
                 questIndicatorCanvasdUI.DestroyItem(item.Key);

                ResourceManager.Instance.UpdateResourceType(ResourceManager.ResourceType.Money, handler.GetQuest(quest).Reward);
                ResourceAnimationManager.Instance.TakeResource(handler.GetQuest(quest).Reward, ResourceManager.ResourceType.Money, buttonPostion);
            }
        }
    }

    public void Load()
    {
        Debug.Log("LoadQuest");
        var data = SaveManager.Load<SaveData.QuestSaveData>(saveKey);
        foreach (var item in handler.questDB.Quests)
        {
            var quest = item.GetQuest(0);
            handler.AssignQuest(quest.Name);
        }
        foreach (var item in data.QuestDataList)
        {
            handler.GetQuest(item.Name).Status = (QQ_QuestStatus)item.State;
            //andler.GetQuest(item.Name).Tasks[0].IncreaseProgress(item.TaskProgress)
            //TaskProgress(handler.GetQuest(item.Name).Tasks[0].Name, item.TaskProgress);
        }

        foreach (var item in handler.Quests)
        {
            var quest = item.Value;
            if (quest.Status == QQ_QuestStatus.Inactive)
            {
                questCanvasUI.CreateItem(quest.Description, quest.Reward, quest.Name);
                Debug.Log(item.Value.Name + " Inactive");
            }
            if (quest.Status == QQ_QuestStatus.Active)
            {
                Debug.Log(item.Value.Name + " Active " + item.Value.Tasks[0].Completed + item.Value.Tasks[0].Progress);
                questCanvasUI.CreateItem(quest.Description, quest.Reward, quest.Name);
                questCanvasUI.TakeQuest(quest.Name);
                questIndicatorCanvasdUI.CreateItem(quest.Name, quest.Description, quest.Tasks[0].MaxProgress, quest.Tasks[0].Progress);
            }

        }
        foreach (var item in data.QuestDataList)
        {
            TaskProgress(handler.GetQuest(item.Name).Tasks[0].Name, item.TaskProgress);
        }

    }

    public void Save()
    {
        Debug.Log("SaveQuest");
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }

    private SaveData.QuestSaveData GetSaveSnapshot()
    {

        var data = new SaveData.QuestSaveData();
        foreach (var kvp in handler.Quests)
        {
            SaveData.QuestData questData = new SaveData.QuestData()
            {
                Name = kvp.Key,
                State = (int)kvp.Value.Status,
                TaskProgress = kvp.Value.Tasks[0].Progress

            };

            data.QuestDataList.Add(questData);
        }
        return data;
    }
}
