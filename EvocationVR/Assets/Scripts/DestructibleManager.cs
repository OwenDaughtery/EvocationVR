using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleManager : MonoBehaviour
{
    public int health;


    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void reduceHealth(int damage) {
        health -= damage;
    }
}
