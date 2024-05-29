using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    public int attackDamage = 40;
    public int attackHydrationCost = 20;
    public int stealthAttackHydrationCost = 5;
    public float attackInterval = 0.5f;
    float nextAttackTime = 0f;

    public AudioClip attackSound;

    public Animator animator;

    PlayerHealth health;

    AudioSource source;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                source.PlayOneShot(attackSound);
                animator.Play("attack");
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
            {
                // Stealth kill
                enemy.Damage(enemy.maxHealth);
                health.TakeHydrationDamage(stealthAttackHydrationCost);
            }
            else
            {
                enemy.Damage(attackDamage);
                health.TakeHydrationDamage(attackHydrationCost);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
