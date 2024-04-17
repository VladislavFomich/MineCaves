using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] private ResourceCanvas resourceCanvas;
    [SerializeField] private ScriptableResource[] scriptableResource;
    Dictionary<ResourceType, int> resourceDictionary = new Dictionary<ResourceType, int>();

    private const string saveKey = "resourceSave";


    public enum ResourceType
    {
        Tree,
        Stone,
        Metal,
        Gold,
        Glass,
        Diamond,
        Money
    }


    public void Awake()
    {
        foreach (var resource in scriptableResource)
        {
            resourceDictionary.Add(resource.ResType, 0);
        }
        Load();
        UpdateAllResourceUI();
    }

    public void UpdateResourceType(ResourceType type)
    {
        resourceDictionary[type] += GetScriptableResource(type).Count;
        resourceCanvas.UpdateResourcesText(type, resourceDictionary[type]);
    }

    public void UpdateResourceType(ResourceType type, int count)
    {
        resourceDictionary[type] += count;
        resourceCanvas.UpdateResourcesText(type, resourceDictionary[type]);
    }

    public void UpdateAllResourceUI()
    {
        foreach (var resourceType in Enum.GetValues(typeof(ResourceType)))
        {
            // Преобразование resourceType к типу ResourceType
            ResourceType type = (ResourceType)resourceType;

            // Получение текущего количества ресурса
            int currentCount = CheckResourceCount(type);

            // Обновление ресурса
            resourceCanvas.UpdateResourcesText(type, resourceDictionary[type]);
        }
    }


    public int CheckResourceCount(ResourceType type)
    {
        return resourceDictionary[type];
    }


    public ScriptableResource GetScriptableResource(ResourceType type)
    {
        return scriptableResource[(int)type];
    }


    public void Load()
    {
        Debug.Log("LoadResource");
        var data = SaveManager.Load<SaveData.ResourceSaveData>(saveKey);
        resourceDictionary[ResourceType.Tree] = data.Tree;
        resourceDictionary[ResourceType.Stone] = data.Stone;
        resourceDictionary[ResourceType.Metal] = data.Metal;
        resourceDictionary[ResourceType.Gold] = data.Gold;
        resourceDictionary[ResourceType.Glass] = data.Glass;
        resourceDictionary[ResourceType.Diamond] = data.Diamond;
        resourceDictionary[ResourceType.Money] = data.Money;
    }

    public void Save()
    {
        Debug.Log("SaveResource");
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }

    private SaveData.ResourceSaveData GetSaveSnapshot()
    {
        var data = new SaveData.ResourceSaveData()
        {
            Tree = resourceDictionary[ResourceType.Tree],
            Stone = resourceDictionary[ResourceType.Stone],
            Metal = resourceDictionary[ResourceType.Metal],
            Gold = resourceDictionary[ResourceType.Gold],
            Glass = resourceDictionary[ResourceType.Glass],
            Diamond = resourceDictionary[ResourceType.Diamond],
            Money = resourceDictionary[ResourceType.Money],
        };
        return data;
    }
}
