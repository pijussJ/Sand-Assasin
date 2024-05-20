using System.Threading.Tasks;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int maxHydration = 100;
    public int currentHydration;

    public HydrationBar hydrationBar;

    private bool isDehydrating = true;

    void Start()
    {
        currentHydration = maxHydration;
        hydrationBar.SetMaxHydration(maxHydration);
        DecreaseHydrationOverTime();
    }

    async void DecreaseHydrationOverTime()
    {
        while (currentHydration > 0 && isDehydrating)
        {
            await Task.Delay(1000); // Wait for 1 second
            TakeDamage(1); // Decrease hydration by 1
        }
    }

    void TakeDamage(int damage)
    {
        currentHydration -= damage;
        if (currentHydration < 0)
        {
            currentHydration = 0;
        }
        hydrationBar.SetHydration(currentHydration);
    }
}