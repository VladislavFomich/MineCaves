using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuildingProgressData", menuName = "Data/BuildingProgressData")]
public class BuildingProgress : ScriptableObject
{
    public int BuildingLevel;
    public GameObject[] BuildingModels;
    public Prices[] Prices;
}
[Serializable]
public class Prices
{
    public int Level;
    public BuildPrice[] BuildPrices;
}