using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public float areaOfEffect = 0.1f;
    public LayerMask whatIsDestructible;
    public int damage = 1;
    //public ObjectPoolerManager objectPool = ObjectPoolerManager.SharedInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag=="Environment") {
            Collider[] objectsToDamage = Physics.OverlapSphere(transform.position, areaOfEffect);
            print(objectsToDamage.Length);
            for (int i = 0; i < objectsToDamage.Length; i++){
                print("enter");
                objectsToDamage[i].GetComponent<DestructibleManager>().reduceHealth(damage);
            }

            ObjectPoolerManager.SharedInstance.returnToPool(this.gameObject, "TestSpell");
        }
    }
}
