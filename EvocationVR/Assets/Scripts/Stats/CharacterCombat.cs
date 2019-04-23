using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{

    public float attackSpeed = 1f;
    private float attackedCooldown = 0f;

    public float attackDelay = 0.6f;
    CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackedCooldown -= Time.deltaTime;
    }

    public void attack(CharacterStats targetStats) {
        if (attackedCooldown <= 0f) {

            print(myStats.damage.getValue());
            StartCoroutine(DoDamage(targetStats, attackDelay));
            attackedCooldown = 1f / attackSpeed;
        }
        
    }

    IEnumerator DoDamage(CharacterStats stats, float delay) {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.getValue());
    }
}
