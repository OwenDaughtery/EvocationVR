using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class MagnitudeManager : MonoBehaviour
{
    EnemyStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = transform.GetComponent<EnemyStats>();
    }

    /// <summary>
    /// Used to detect when this object has come into contact with another object
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "TestSpell" && stats.currentHealth >= 0)
        {

            if (this.GetComponent<Rigidbody>().velocity.magnitude > stats.armour.getValue())
            {
                stats.TakeDamage(this.GetComponent<Rigidbody>().velocity.magnitude, collision.transform.name);
            }
            else if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude > stats.armour.getValue())
            {

                stats.TakeDamage(collision.transform.GetComponent<Rigidbody>().velocity.magnitude, collision.transform.name);
            }
        }
    }
}
