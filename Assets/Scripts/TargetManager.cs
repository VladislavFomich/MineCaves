using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : Singleton<TargetManager>
{
    public List<Transform> targetWorld;

    private void Awake()
    {
        var resources = FindObjectsOfType<Resource>();
        foreach (var resource in resources) { targetWorld.Add(resource.transform); }
    }

    public void AddObjectToList(Transform transform)
    {
        targetWorld.Add(transform);
    }
    public void RemoveObjectFromList(Transform transform)
    {

        targetWorld.Remove(transform);
    }


    public Transform GetRandomDestination()
    {
        if(targetWorld.Count == 0) return null;
        else return targetWorld[Random.Range(0, TargetManager.Instance.targetWorld.Count)];
    }
}
