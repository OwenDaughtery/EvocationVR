using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSpellManager : SpellManager
{
    float growthRate = 0.1f;

    

    // Update is called once per frame
    void Update()
    {
        base.Update();
        expandForceField(growthRate);
    }

    void expandForceField(float growthRate) {
        transform.localScale += Vector3.one * growthRate;
    }
}
