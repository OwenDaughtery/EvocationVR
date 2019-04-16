using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anyKeyPressed();
    }

    private void anyKeyPressed() {
        if (Input.GetKeyDown("e"))
        {
            shootSpell();
        }
    }

    private void shootSpell()
    {
        
        GameObject testSpell = ObjectPoolerManager.SharedInstance.GetPooledObject("TestSpell");
        if (testSpell != null)
        {
            //set position of bullet
            testSpell.transform.position = this.transform.position;
            testSpell.SetActive(true);


        }

    }
}
