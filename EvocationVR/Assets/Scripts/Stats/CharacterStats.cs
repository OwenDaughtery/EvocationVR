using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public float maxHealth = 100;
    [SerializeField]
    public float currentHealth;

    public Stat damage;
    public Stat armour;

    private void Awake()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(float damage) {
        
        damage -= armour.getValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage);
        if (currentHealth <= 0) {
            die();
        }
    }

    public virtual void die() {
        // die in some way
        //this method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }
}
