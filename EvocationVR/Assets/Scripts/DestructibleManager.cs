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

    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.tag != "TestSpell" && health>=0) {

            if (this.GetComponent<Rigidbody>().velocity.magnitude > hardness)
            {

                reduceHealth(this.GetComponent<Rigidbody>().velocity.magnitude, this.GetComponent<Rigidbody>().velocity.magnitude);
            }
            else if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude > hardness) {

                reduceHealth(collision.transform.GetComponent<Rigidbody>().velocity.magnitude, collision.transform.GetComponent<Rigidbody>().velocity.magnitude);
            }
        }
    }

    public void reduceHealth(float damage, float magnitude) {

        if (magnitude >= hardness) {
            health -= damage;
        }
        checkIfDead();
    }
}
