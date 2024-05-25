using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    public int attackDamage = 40;
    public float attackInterval = 0.5f;
    float nextAttackTime = 0f;
    
    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + attackInterval;
            }
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider hitEnemy in hitEnemies)
        {
            Enemy enemy = hitEnemy.GetComponent<Enemy>();
            if (!hitEnemy.GetComponent<EnemyFieldOfView>().canSeePlayer)
                enemy.Damage(enemy.maxHealth); // Stealth kill
            else
                enemy.Damage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
