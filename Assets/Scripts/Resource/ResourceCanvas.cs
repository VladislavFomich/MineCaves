using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Cecil;

public class ResourceCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text treeText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField] private TMP_Text metalText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text glassText;
    [SerializeField] private TMP_Text diamondText;

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
        }
    }
}
