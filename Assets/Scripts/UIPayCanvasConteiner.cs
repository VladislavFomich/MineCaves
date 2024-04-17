using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;


public class UIPayCanvasConteiner : MonoBehaviour
{
    [SerializeField] private ResourcePayUI resourcePayUI;
    [SerializeField] private BuildingTitleUI title;
    [SerializeField] private Transform conteiner;
    [SerializeField] private float destroyAnimationSpeed = 05f;
    [SerializeField] private Building mainPrefab;
    [SerializeField] private string titleText;
    private List<ResourcePayUI> resourcePayList = new List<ResourcePayUI>();

    public void Init()
    {
        var txt = Instantiate(title, conteiner);
        txt.title.text = titleText;
    }

    public void SpawnPayCell(ResourceManager.ResourceType type, int count)
    {
        ResourcePayUI res = Instantiate(resourcePayUI, conteiner);
        res.text.text = count.ToString(); 
        res.image.sprite = ResourceManager.Instance.GetScriptableResource(type).Icon;
        res.type = type;
        resourcePayList.Add(res);
    }


    public void DestroyCell(ResourceManager.ResourceType type)
    {
        foreach (var item in resourcePayList)
        {
            if(item != null && item.type == type)
            {
                item.transform.DOScale(Vector3.zero, destroyAnimationSpeed)
                     .OnComplete(() => CompleteDestroyCell(item));
            }
        }
    }


    public void ChangeItemCount(ResourceManager.ResourceType type)
    {
        foreach(var item in resourcePayList)
        {
            if (item != null && item.type == type)
            {
                int count;
                if (int.TryParse(item.text.text, out count))
                {
                    count -= 1;
                    item.text.text = count.ToString();
                }
            }
        }
    }


    private void CompleteDestroyCell(ResourcePayUI resouercePay)
    {
        Destroy(resouercePay.gameObject);
        resourcePayList.Remove(resouercePay);
        if(resourcePayList.Count <= 0)
        {
            mainPrefab.gameObject.SetActive(false);
        }
    }
}
