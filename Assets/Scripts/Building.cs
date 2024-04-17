using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Building : MonoBehaviour
{
    public List<LevelBuildPrice> LevelBuildPrice = new List<LevelBuildPrice>();

    [SerializeField] private GameObject[] models;
    [SerializeField] private UIPayCanvasConteiner canvasPayConteiner;
    [SerializeField] private BuildingType currentType = BuildingType.Forge;

    private bool _startCorrutine = false;
    private bool _isRecovery = false;
    private int _payedCount;
    private int _currentLevel;
    private  string saveKey;

    public enum BuildingType
    {
        Forge,
    }

    private void Awake()
    {
        saveKey = "buildingSave" + currentType.ToString();
        Load();
        ActivateModel();
        if (_currentLevel > LevelBuildPrice.Count - 1) { gameObject.SetActive(false); return; }
        canvasPayConteiner.Init();
        foreach (var item in LevelBuildPrice[0].buildPrice)
        {
            canvasPayConteiner.SpawnPayCell(item.resourceType, item.price);
        }
    }


    public void Interact(bool isPlayerInArea)
    {
        if(_currentLevel > LevelBuildPrice.Count - 1) { return; }
        if (!_isRecovery)
        {
            if (isPlayerInArea)
            {
                _payedCount = 0;
                foreach (var item in LevelBuildPrice[_currentLevel].buildPrice)
                {
                    if (item.price != 0)
                    {
                        _payedCount++;
                    }
                }
                _startCorrutine = true;
                StartCoroutine(BuildingHouse());


            }
            else
            {
                _startCorrutine = false;
                StopAllCoroutines();
            }
        }
        else
        {
            if (isPlayerInArea)
            {
                //Popup.SetActive(true);
            }
        }
    }


    private void ChangeBuildModel()
    {
        StopAllCoroutines();
        _isRecovery = true;
        _startCorrutine = false;
        ActivateModel();
    }


    private void ActivateModel()
    {
        foreach (var item in models)
        {
            item.SetActive(false);
        }
        models[_currentLevel].SetActive(true);
    }

    IEnumerator BuildingHouse()
    {

        while (_payedCount > 0)
        {
            foreach (var item in LevelBuildPrice[_currentLevel].buildPrice)
            {
                if (ResourceManager.Instance.CheckResourceCount(item.resourceType) != 0 && item.price != 0)
                {
                    item.price--;
                    ResourceManager.Instance.UpdateResourceType(item.resourceType, -1);
                    ResourceAnimationManager.Instance.SpendResource(1, item.resourceType, models[_currentLevel].transform.position);
                    canvasPayConteiner.ChangeItemCount(item.resourceType);
                    if (item.price <= 0)
                    {
                        canvasPayConteiner.DestroyCell(item.resourceType);
                        _payedCount--;
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (_payedCount <= 0)
        {
            _currentLevel++;
            ChangeBuildModel();
        }
        Save();
        ResourceManager.Instance.Save();
        yield return new WaitForSeconds(0.1f);
    }

    public void Save()
    {
        Debug.Log("SaveBuilding");
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }


    private SaveData.BuildingSaveData GetSaveSnapshot()
    {
        var data = new SaveData.BuildingSaveData()
        {
            LevelBuildPrice = LevelBuildPrice,
            Level = _currentLevel
            
        };
        return data;
    }


    public void Load()
    {
        Debug.Log("LoadBuilding");
        var data = SaveManager.Load<SaveData.BuildingSaveData>(saveKey);
        if(data.LevelBuildPrice.Count != 0)
        {
            LevelBuildPrice = data.LevelBuildPrice;
            _currentLevel = data.Level;
        }
    }
}
[Serializable]
public class BuildPrice
{
    public ResourceManager.ResourceType resourceType;
    public int price;
}

[Serializable]
public class LevelBuildPrice
{
    public int Level;
    public List<BuildPrice> buildPrice;
}
