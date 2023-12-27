using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCanvas : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_Text discriptions;
    [SerializeField] private Button extButton;


    public void SetInfo(ScriptableWeapon item, PickableItem pickableItem)
    {
        image.sprite = item.Icon;
        header.text = item.Name;
        discriptions.text = item.Discription;
        extButton.onClick.AddListener(() => pickableItem.CloseItemCanvas());
    }


    public void UnSubcribeButton(PickableItem pickableItem)
    {
        extButton.onClick.RemoveListener(() => pickableItem.CloseItemCanvas());
    }
}
