using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int buildCost;
    [SerializeField] private GameObject oldBuild;
    [SerializeField] private GameObject newBuild;
    private bool _startCorrutine = false;
    private bool _isRecovery = false;

    public GameObject Popup;

    public void Interact(bool isPlayerInArea)
    {
        if (!_isRecovery)
        {
            if (isPlayerInArea)
            {
                _startCorrutine = true;
                StartCoroutine(BuildingHouse(1));


            }
            else
                _startCorrutine = false;
        }
        else
        {
            if (isPlayerInArea)
            {
                Popup.SetActive(true);
            }
        }
    }


    private void ChangeBuildModel()
    {
        StopCoroutine(BuildingHouse(1));
        _isRecovery = true;
        _startCorrutine = false;
        oldBuild.SetActive(false);
        newBuild.SetActive(true);
    }


    IEnumerator BuildingHouse(int recource)
    {
        
        while (ResourceManager.Instance.CheckResourceCount(ResourceManager.ResourceType.Tree) != 0 && _startCorrutine == true)
        {
            buildCost -= recource;
            ResourceManager.Instance.UpdateResourceType(ResourceManager.ResourceType.Tree, 1);
            if (buildCost == 0)
            {
                ChangeBuildModel();
            }
        }
       
            yield return new WaitForSeconds(0.1f);
    }
}
