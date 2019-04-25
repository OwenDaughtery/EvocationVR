using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class ForceSpellManager : SpellManager
{

    
    float growthRate = 0.4f;
    Vector3 originalScale;
    public float force = 1f;

    private void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }

    private void Awake()
    {
        originalScale = transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        expandForceField(growthRate);
        updatePosition();
    }

    public override void deactivated()
    {
        base.deactivated();
        transform.localScale = originalScale;
    }

    public override void triggerCollided(Collider other)
    {
        base.triggerCollided(other);
        applyForcePush(other);
        
    }

    void applyForcePush(Collider other) {
        Vector3 center = transform.position;
        Vector3 contactedAt = transform.GetComponent<CapsuleCollider>().ClosestPointOnBounds(other.transform.position);
        Vector3 pushedDirection = (contactedAt - center).normalized;
        //Debug.Log("center: " + center);
        //Debug.Log("contacted at: " + contactedAt);

        other.gameObject.GetComponent<Rigidbody>().velocity = (pushedDirection * force)/other.gameObject.GetComponent<Rigidbody>().mass;

        //other.gameObject.transform.LookAt(-transform.position);
        //other.gameObject.transform.Translate(0.0f, 0.0f, 15 * Time.deltaTime);

        //edge - center will give a vector.
    }



    void expandForceField(float growthRate) {
        transform.localScale += new Vector3(1f,0f,1f) * growthRate;
    }

    void updatePosition() {
        transform.position = PlayerManager.instance.player.transform.position;
    }
}
