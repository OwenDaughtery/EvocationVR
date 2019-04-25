﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public float force = 1f;
    public int damage = 1;
    public float maxLifeTime;
    public float currentLifeTime;
    public string tag;
    [SerializeField]
    public List<string> tagsToIngore = new List<string>();
    public List<int> layersToIgnore = new List<int>();
    

    //public ObjectPoolerManager objectPool = ObjectPoolerManager.SharedInstance;

    private void OnEnable()
    {
        currentLifeTime = 0f;

    }

    // Start is called before the first frame update
    void Start()
    {
        tag = this.gameObject.transform.tag;
    }

    // Update is called once per frame
    public void Update()
    {
        checkActiveTime();
    }

    public virtual void deactivate() {
        ObjectPoolerManager.SharedInstance.returnToPool(this.gameObject, transform.tag);
        //Debug.Log("deactivating spell " + transform.name);
    }

    private void checkActiveTime() {

        /*print(Time.deltaTime);
        print(timeActivated + lifeTime);
        print("");*/
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime>=maxLifeTime) {
            ////StartCoroutine(collided());
            //ObjectPoolerManager.SharedInstance.returnToPool(this.gameObject, "TestSpell");
            deactivate();
        }
    }

    /*private void OnCollisionEnter(Collision other)
    {
        

        //if the item is destructible
        if (other.gameObject.layer==10) {
            
            //Debug.Log(other.transform.name);
            deactivate();
            other.gameObject.GetComponent<DestructibleManager>().reduceHealth(damage, int.MaxValue);
            
        }

        //if the item is an enemy
        else if (other.gameObject.tag == "Enemy") {
            //Debug.Log(other.transform.name);
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage, transform.gameObject.name);
        }

        //if the item is something that should destroy the spell
        if (!tagsToIngore.Contains(other.gameObject.tag)) {
            //Debug.Log(other.transform.name);
            if (!layersToIgnore.Contains(other.gameObject.layer)) {
                ////StartCoroutine(collided());
                deactivate();
            }

        }
        
    }*/

    public virtual void triggerCollided(Collider other) {
        Debug.Log("Trigger collided with something at: " + other.transform.position + " with name: " + other.transform.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool deactivateObject = false;
        //if the item is destructible
        if (other.gameObject.layer == 10)
        {
            print("1");
            other.gameObject.GetComponent<DestructibleManager>().reduceHealth(damage, int.MaxValue);//hard coded as max value to get past hardnesses.
            triggerCollided(other);
            //deactivateObject = true;
        }
        //Or the item is on layer object
        else if (other.gameObject.layer == 12)
        {
            triggerCollided(other);
            print("2");
            //deactivateObject = true;
        }

        //if the item is on enemy layer
        else if (other.gameObject.layer == 13)
        {
            print("3");
            triggerCollided(other);
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage, transform.gameObject.name);
            //deactivateObject = true;
        }//or something that should destroy the spells

        if (!tagsToIngore.Contains(other.gameObject.tag)) {
            print("4");
            if (!layersToIgnore.Contains(other.gameObject.layer)) {
                ////StartCoroutine(collided());
                deactivateObject = true;
            }

        }

        if (deactivateObject) {
            deactivate();
        }
    }




}
