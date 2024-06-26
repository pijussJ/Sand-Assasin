using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public int maxHydration = 100;
    public int currentHydration;

    public int constantHydrationDamage = 1;
    public int sandstormHydrationDamage = 3;
    public float constantHydrationDamageInterval = 2f;

    public int passiveHealingAmount = 2;
    public float hydrationHealingThresholdPercentage = 70;

    public HydrationBar hydrationBar;
    public HydrationBar healthBar;

    public string nextSceneName;

    public AudioClip rehydrationSound;
    public AudioClip takeDamageSound;
    public AudioClip spawnSound;

    AudioSource source;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHydration(maxHealth);
        currentHydration = maxHydration;
        hydrationBar.SetMaxHydration(maxHydration);

        InvokeRepeating("TakeConstantHydrationDamage", constantHydrationDamageInterval, constantHydrationDamageInterval);

        source = GetComponent<AudioSource>();
        source.PlayOneShot(spawnSound);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (damage > 0)
            source.PlayOneShot(takeDamageSound);

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
        int damage;
        if (Sandstorm.isSandstorm)
            damage = sandstormHydrationDamage;
        else
            damage = constantHydrationDamage;

        TakeHydrationDamage(damage);

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
            source.PlayOneShot(rehydrationSound);
        }
        if (other.gameObject.transform.parent.name.Contains("Teleporter"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
