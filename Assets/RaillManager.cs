using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaillManager : MonoBehaviour
{
    private RaillActivator[] _railActivators;
    private const string saveKey = "RaillSave";

    private void Awake()
    {
        _railActivators = GetComponentsInChildren<RaillActivator>();
        Load();
        foreach (var item in _railActivators)
        {
            item.Init();
            item.ActivateAction += Save;
        }
    }


    public void Save()
    {
        Debug.Log("SaveRail");
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }


    private SaveData.RaillSaveData GetSaveSnapshot()
    {
        List<bool> boolArray = new List<bool>();
        foreach (var rail in _railActivators)
        {
            boolArray.Add(rail.IsActivated);
        }
        var data = new SaveData.RaillSaveData()
        {
            ActivateRaill = boolArray
        };
        return data;
    }


    public void Load()
    {
        Debug.Log("LoadRaill");
        var data = SaveManager.Load<SaveData.RaillSaveData>(saveKey);
        if(data.ActivateRaill != null)
        {
            for (int i = 0; i < data.ActivateRaill.Count; i++)
            {
                _railActivators[i].IsActivated = data.ActivateRaill[i];
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var item in _railActivators)
        {
            item.ActivateAction -= Save;
        }
    }
}
