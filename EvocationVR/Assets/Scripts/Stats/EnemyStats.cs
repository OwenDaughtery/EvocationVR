using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public string itemToReplaceWith;

    public override void die()
    {
        base.die();
        GameObject destroyedItem = ObjectPoolerManager.SharedInstance.GetPooledObject(itemToReplaceWith);
        destroyedItem.transform.position = this.gameObject.transform.position; //+ new Vector3(0f,0f,0f);
        destroyedItem.transform.rotation = this.gameObject.transform.rotation;

        destroyedItem.transform.parent = null;

        //trying to copy over velocity from old object
        foreach (Transform child in destroyedItem.gameObject.transform)
        {
            child.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
        }

        Destroy(gameObject);

        destroyedItem.SetActive(true);
        
        //die in a cool way
    }

}
