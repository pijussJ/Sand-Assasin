using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int maxHydration = 100;
    public int currentHydration;

    public HydrationBar hydrationBar;
    void Start()
    {
        currentHydration = maxHydration;
        hydrationBar.SetMaxHydration(maxHydration);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHydration -= damage;

        hydrationBar.SetHydration(currentHydration);
    }

}
