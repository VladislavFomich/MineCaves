using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour
{
    public TMP_Text discription;
    public TMP_Text reward;
    public string QuestID;
    public Button buttonGetQuest;
    public Button buttonGetReward;

    [SerializeField] private Sprite unInteractableBtnSprite;

    public void TakeQuest()
    {
        buttonGetQuest.interactable = false;
        buttonGetQuest.image.sprite = unInteractableBtnSprite;
    }

    public void CompleteQuest()
    {
        buttonGetQuest.gameObject.SetActive(false);
        buttonGetReward.gameObject.SetActive(true);
    }
}
