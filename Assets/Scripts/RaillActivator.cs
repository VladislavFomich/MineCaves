using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RaillActivator : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private float timeBeforePay = 2;
    [SerializeField] private RailContainer railContainer;
    [SerializeField] private ResourcePayUI resourcePayUI;

    public bool IsActivated;
    public UnityAction ActivateAction;

    public void Init()
    {
        if (IsActivated)
        {
            railContainer.gameObject.SetActive(true);
            gameObject.SetActive(false);
            return;
        }
        resourcePayUI.text.text = price.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<WaitIndicator>();
        if(player != null)
        {
            if(price <= ResourceManager.Instance.CheckResourceCount(ResourceManager.ResourceType.Money))
            {
                player.StartWait(timeBeforePay);
                StartCoroutine(StartPay());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<WaitIndicator>();
        if (player != null)
        {
            StopAllCoroutines();
            player.StopWait();
        }
    }


    IEnumerator StartPay()
    {
        yield return new WaitForSeconds(timeBeforePay);
        ResourceManager.Instance.UpdateResourceType(ResourceManager.ResourceType.Money, -price);
        Vector3 moneyTarget = new Vector3(railContainer.rails[0].transform.position.x - 3f, railContainer.rails[0].transform.position.y, railContainer.rails[0].transform.position.z);
        ResourceAnimationManager.Instance.SpendResource(price, ResourceManager.ResourceType.Money, moneyTarget);
        railContainer.gameObject.SetActive(true);
        ResourceManager.Instance.Save();
        IsActivated = true;
        ActivateAction?.Invoke();
        gameObject.SetActive(false);
    }
}
