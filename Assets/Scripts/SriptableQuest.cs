using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestData", menuName = "QuestList")]
public class SriptableQuest : ScriptableObject
{
    [SerializeField] private Quest[] questsList;

    public Quest[] QuestList { get => questsList; }

    [Serializable]
    public class Quest
    {
        public string Description;
        public int Reward;
        public int QuestID;
    }
}
