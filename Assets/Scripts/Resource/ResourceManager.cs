using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] private ResourceCanvas resourceCanvas;
    [SerializeField] private ScriptableResource[] scriptableResource;
    Dictionary<ResourceType, int> resourceDictionary = new Dictionary<ResourceType, int>();



    public enum ResourceType
    {
        Tree,
        Stone,
        Metal,
        Gold, 
        Glass,
        Diamond
    }


    public void Init()
    {
        foreach (var resource in scriptableResource)
        {
            resourceDictionary.Add(resource.ResType, 0);
        }
    }

    public void UpdateResourceType(ResourceType type)
    {
        resourceDictionary[type] += GetScriptableResource(type).Count;
        resourceCanvas.UpdateResourcesText(type, resourceDictionary[type]);
    }

    public void UpdateResourceType(ResourceType type, int minusCount)
    {
        resourceDictionary[type] -= minusCount;
        resourceCanvas.UpdateResourcesText(type, resourceDictionary[type]);
    }



    public int CheckResourceCount(ResourceType type)
    {
        return resourceDictionary[type];
    }

    public ScriptableResource GetScriptableResource(ResourceType type)
    {
        return scriptableResource[(int)type];
    }
}
