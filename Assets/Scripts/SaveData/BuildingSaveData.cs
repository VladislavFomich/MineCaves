
using System;
using System.Collections.Generic;

namespace SaveData
{
    [Serializable]
    public class BuildingSaveData
    {
        public int Level;
        public List<LevelBuildPrice> LevelBuildPrice = new List<LevelBuildPrice>();
    }
}