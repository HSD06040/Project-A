using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestData",menuName = "Data/ChestData")]
public class ChestData : ScriptableObject
{
    public Material material;

    public float unCommonChance;
    public float rareChance;
    public float epicChance;
    public float uniqueChance;
    public float legendaryChance;
}
