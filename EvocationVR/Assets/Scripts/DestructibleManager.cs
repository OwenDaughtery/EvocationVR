using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleManager : MonoBehaviour
{
    public float health;
    public float hardness;
    public string itemToReplaceWith;


    // Update is called once per frame
    void Update()
    {
    }

    public void checkIfDead() {
        if (health <= 0){
            GameObject destroyedItem = ObjectPoolerManager.SharedInstance.GetPooledObject(itemToReplaceWith);
            print(destroyedItem);
            destroyedItem.transform.position = this.gameObject.transform.position; //+ new Vector3(0f,0f,0f);
            destroyedItem.transform.rotation = this.gameObject.transform.rotation;

            destroyedItem.transform.parent = null;
            
            //trying to copy over velocity from old object
            foreach (Transform child in destroyedItem.gameObject.transform) {
                child.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
            }

            Destroy(gameObject);

            destroyedItem.SetActive(true);
        }

    }

    /// <summary>
    /// Called when this object has touched something, should check to see if that thing was going fast or if this object was going fast.
    /// </summary>
    /// <param name="collision">The Collision the object has touched</param>
    public void OnCollisionEnter(Collision collision)
    {
        //If the object was a spell, forget about the collision, the spell will deal it's own damage and force appliance.
        if (collision.gameObject.layer != 9 && health>=0) {
            //Debug.Log("collision with: " + collision.gameObject.name);
            if (this.GetComponent<Rigidbody>().velocity.magnitude > hardness)
            {
                //Debug.Log("hurt by self by: " + this.GetComponent<Rigidbody>().velocity.magnitude);
                reduceHealth(this.GetComponent<Rigidbody>().velocity.magnitude, this.GetComponent<Rigidbody>().velocity.magnitude);
            }
            else if(collision.transform.GetComponent<Rigidbody>() != null) { 
                if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude > hardness) {
                    //Debug.Log("hurt by other by: " + collision.transform.GetComponent<Rigidbody>().velocity.magnitude);
                    reduceHealth(collision.transform.GetComponent<Rigidbody>().velocity.magnitude, collision.transform.GetComponent<Rigidbody>().velocity.magnitude);
                }
            }
        }
    }

    /// <summary>
    /// called when this object hits something hard or when an object hits this object hard.
    /// Or when a spell hits this object, if its a spell, magnitude will always be maximum as spells should always do damage.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="magnitude"></param>
    public void reduceHealth(float damage, float magnitude) {
        if (magnitude >= hardness) {
            health -= damage;
        }
        checkIfDead();
    }
}
