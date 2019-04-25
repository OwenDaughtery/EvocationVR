using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpellManager : SpellManager { 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        base.Update();
        
    }

    public override void triggerCollided(Collider other)
    {
        base.triggerCollided(other);
        applyForcePush(other);

    }

    void applyForcePush(Collider other)
    {

        Vector3 pushedDirection = transform.GetComponent<Rigidbody>().velocity.normalized;
        other.gameObject.GetComponent<Rigidbody>().velocity = (pushedDirection * force) / other.gameObject.GetComponent<Rigidbody>().mass;
        /*Vector3 center = transform.position;
        Vector3 contactedAt = transform.GetComponent<SphereCollider>().ClosestPointOnBounds(other.transform.position);
        Vector3 pushedDirection = (contactedAt - center).normalized;
        Debug.Log("center: " + center);
        Debug.Log("contacted at: " + contactedAt);


        other.gameObject.GetComponent<Rigidbody>().velocity = (pushedDirection * force) / other.gameObject.GetComponent<Rigidbody>().mass;*/

        //other.gameObject.transform.LookAt(-transform.position);
        //other.gameObject.transform.Translate(0.0f, 0.0f, 15 * Time.deltaTime);

        //edge - center will give a vector.
    }
}
