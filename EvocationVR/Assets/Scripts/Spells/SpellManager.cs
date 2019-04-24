using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public float areaOfEffect = 0.01f;
    public LayerMask whatIsDestructible;
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

    public virtual void deactivated() {
        //Debug.Log("deactivating spell " + transform.name);
    }

    private void checkActiveTime() {

        /*print(Time.deltaTime);
        print(timeActivated + lifeTime);
        print("");*/
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime>=maxLifeTime) {
            StartCoroutine(collided());
            //ObjectPoolerManager.SharedInstance.returnToPool(this.gameObject, "TestSpell");

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.layer==10) {
            other.gameObject.GetComponent<DestructibleManager>().reduceHealth(damage, this.GetComponent<Rigidbody>().velocity.magnitude);
        }
        if (other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
        }

        if (!tagsToIngore.Contains(other.gameObject.tag)) {
            if (!layersToIgnore.Contains(other.gameObject.layer)) {
                StartCoroutine(collided());
            }
            
        }
        
    }

    public virtual void triggerCollided(Collider other) {
        Debug.Log("Trigger collided with something at: " + other.transform.position + " with name: " + other.transform.name);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 10)
        {
            triggerCollided(other);
            other.gameObject.GetComponent<DestructibleManager>().reduceHealth(damage, 20f);//hard coded as 20 to get past (most) hardnesses.
        }

        if (other.gameObject.tag == "Enemy")
        {
            triggerCollided(other);
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
        } else if (!tagsToIngore.Contains(other.gameObject.tag)) {
            if (!layersToIgnore.Contains(other.gameObject.layer)) {
                triggerCollided(other);
                StartCoroutine(collided());
            }
            
        }
    }

    IEnumerator collided(){
        yield return new WaitForEndOfFrame();
        if (gameObject.GetComponent<MeshRenderer>().enabled) {

        
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            int numberOfChildren = this.transform.childCount;
            for (int i = 0; i < numberOfChildren; i++)
            {
                GameObject child = this.transform.GetChild(i).gameObject;
                child.SetActive(false);
            }
            yield return new WaitForEndOfFrame();
            ObjectPoolerManager.SharedInstance.returnToPool(this.gameObject, transform.tag);
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            for (int i = 0; i < numberOfChildren; i++)
            {
                GameObject child = this.transform.GetChild(i).gameObject;
                child.SetActive(true);
            }

            deactivated();
            
        }
        StopCoroutine(collided());
    }


}
