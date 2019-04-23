using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private float baseValue;

    private List<int> modifiers = new List<int>();

    public float getValue() {
        float finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void addModifier(int modifier) {
        if(modifier != 0) {
            modifiers.Add(modifier);    
        }
    }

    public void removeModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }

}
