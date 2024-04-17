
using QuantumTek.QuantumQuest;
using System;
using System.Collections.Generic;

namespace SaveData
{
    [Serializable]
    public class QuestSaveData
    {
        public List<QuestData> QuestDataList = new List<QuestData>();
    }

    [Serializable]
    public class QuestData
    {
        public string Name;
        public int State;
        public float TaskProgress;
    }
}
