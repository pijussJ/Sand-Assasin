using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;

    public int damageToPlayer;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayers;
    public float attackInterval;
    float nextAttackTime;

    //EnemyFieldOfView detection;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        print(currentHealth);
        if (currentHealth <= 0)
            Die();
    }

    void Attack()
    {
        if (Time.time < nextAttackTime)
            return;

        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayers);
        hitPlayer[0].GetComponent<PlayerHealth>().TakeDamage(damageToPlayer);
        nextAttackTime = Time.time + attackInterval;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
