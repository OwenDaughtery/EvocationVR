using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public float maxMana = 10f;
    public float availableMana;
    public float regenRate;

    // Start is called before the first frame update
    void Start()
    {
        availableMana = maxMana;
        regenRate = maxMana / 500;
    }

    // Update is called once per frame
    void Update()
    {
        regenMana(regenRate);
    }

    void regenMana(float regenRate) {
        if (availableMana + regenRate < maxMana)
        {
            availableMana += regenRate;
        }
        else {
            availableMana = maxMana;
        }
    }

    public bool subtractMana(float spellCost) {
        if (availableMana - spellCost < 0)
        {
            return false;
        }
        else {
            availableMana -= spellCost;
            return true;
        }
    }
}
