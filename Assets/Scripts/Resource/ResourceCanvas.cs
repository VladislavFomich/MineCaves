using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Cecil;
using UnityEngine.UI;

public class ResourceCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text treeText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField] private TMP_Text metalText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text glassText;
    [SerializeField] private TMP_Text diamondText;

    [SerializeField] private Image moneyImg;
    [SerializeField] private Image treeImg;
    [SerializeField] private Image stoneImg;
    [SerializeField] private Image metalImg;
    [SerializeField] private Image goldImg;
    [SerializeField] private Image glassImg;
    [SerializeField] private Image diamondImg;

    public void UpdateResourcesText(ResourceManager.ResourceType resourceType, int count)
    {
        switch (resourceType)
        {
            case ResourceManager.ResourceType.Tree:
                treeText.text = count.ToString();
                break;
            case ResourceManager.ResourceType.Stone:
                stoneText.text = count.ToString();
                break;
            case ResourceManager.ResourceType.Metal:
                metalText.text = count.ToString();
                break;
            case ResourceManager.ResourceType.Gold:
                goldText.text = count.ToString();
                break;
            case ResourceManager.ResourceType.Glass:
                glassText.text = count.ToString();
                break;
            case ResourceManager.ResourceType.Diamond:
                diamondText.text = count.ToString();
                break;
            case ResourceManager.ResourceType.Money:
                moneyText.text = count.ToString();
                break;
        }
    }


    public Image GetImage(ResourceManager.ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceManager.ResourceType.Tree:
               return treeImg;
            case ResourceManager.ResourceType.Stone:
                return stoneImg;
            case ResourceManager.ResourceType.Metal:
                return metalImg;
            case ResourceManager.ResourceType.Gold:
                return goldImg;
            case ResourceManager.ResourceType.Glass:
                return glassImg;
            case ResourceManager.ResourceType.Diamond:
                return diamondImg;
            case ResourceManager.ResourceType.Money:
                return moneyImg;

            default: return null;
        }
    }
}
