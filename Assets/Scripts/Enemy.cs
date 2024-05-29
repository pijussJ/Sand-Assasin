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
    float attackStartRange = 3f;

    public AudioClip takeDamageSound;
    public AudioClip deathSound;
    public AudioClip attackSound;

    EnemyFieldOfView fov;

    AudioSource source;

    private void Start()
    {
        currentHealth = maxHealth;
        fov = GetComponent<EnemyFieldOfView>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (fov.canSeePlayer && Vector3.Distance(attackPoint.position, fov.player.transform.position) <= attackStartRange + attackRange)
        {
            Attack();
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
        else
            source.PlayOneShot(takeDamageSound);
    }

    void Attack()
    {
        if (Time.time < nextAttackTime)
            return;

        // PLAY ANIMATION and maybe sound

        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayers);
        if (hitPlayer.Length <= 0)
            return;

        hitPlayer[0].GetComponentInParent<PlayerHealth>().TakeDamage(damageToPlayer);

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
        source.PlayOneShot(deathSound);
        Destroy(gameObject);
    }
}