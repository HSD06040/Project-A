using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

[Serializable]
public class Stat
{
    public int baseValue;
    public List<int> modifiers;

    public int GetValue()
    {
        int totalValue = baseValue;

        foreach (int modifier in modifiers)
        {
            totalValue += modifier;
        }

        return totalValue;
    }

    public void AddModifier(int value) => modifiers.Add(value);

    public void RemoveModifier(int value) => modifiers.Remove(value);

    public void SetBaseValue(int value) => baseValue = value;
}
