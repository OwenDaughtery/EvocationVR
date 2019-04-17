using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public float areaOfEffect = 0.01f;
    public LayerMask whatIsDestructible;
    public int damage = 1;
    public float maxLifeTime = 0.2f;
    public float currentLifeTime;
    public string tag;

    //public ObjectPoolerManager objectPool = ObjectPoolerManager.SharedInstance;

    private void OnEnable()
    {
        currentLifeTime = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkActiveTime();
    }

    private void checkActiveTime() {

        /*print(Time.deltaTime);
        print(timeActivated + lifeTime);
        print("");*/
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime>=maxLifeTime) {
            ObjectPoolerManager.SharedInstance.returnToPool(this.gameObject, "TestSpell");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag=="Environment") {
            other.gameObject.GetComponent<DestructibleManager>().reduceHealth(damage, this.GetComponent<Rigidbody>().velocity.magnitude);
        }
        if (other.gameObject.tag != "Player") {
            StartCoroutine(collided());
        }
        
    }

    IEnumerator collided(){
        yield return new WaitForEndOfFrame();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForEndOfFrame();
        ObjectPoolerManager.SharedInstance.returnToPool(this.gameObject, "TestSpell");
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        StopCoroutine(collided());
    }


}
