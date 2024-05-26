using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public int maxHydration = 100;
    public int currentHydration;

    public int constantHydrationDamage = 1;
    public float constantHydrationDamageInterval = 2f;

    public int passiveHealingAmount = 2;
    public float hydrationHealingThresholdPercentage = 70;

    public HydrationBar hydrationBar;
    public HydrationBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHydration(maxHealth);
        currentHydration = maxHydration;
        hydrationBar.SetMaxHydration(maxHydration);

        InvokeRepeating("TakeConstantHydrationDamage", constantHydrationDamageInterval, constantHydrationDamageInterval);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // If player overheals
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        // Update health bar
        healthBar.SetHydration(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void TakeHydrationDamage(int damage)
    {
        currentHydration -= damage;

        // If player overhydrates
        if (currentHydration > maxHydration)
            currentHydration = maxHydration;

        hydrationBar.SetHydration(currentHydration);

        // Player has only just become dehydrated
        if (currentHydration <= 0 && currentHydration + damage > 0)
            return;

        // Player is already dehydrated
        if (currentHydration <= 0)
        {
            currentHydration = 0;
            TakeDamage(damage);
        }
    }

    void TakeConstantHydrationDamage()
    {
        TakeHydrationDamage(constantHydrationDamage);

        // Passive healing
        if ((float)currentHydration / maxHydration >= hydrationHealingThresholdPercentage / 100f)
        {
            TakeDamage(-passiveHealingAmount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("HydrationTrigger"))
        {
            TakeHydrationDamage(-maxHydration);

            // Play rehydration sound
        }
    }

    void Die()
    {

    }
}
