using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxHydration(int hydration)
    {
        slider.maxValue = hydration;
        slider.value = hydration;
    }

    public void SetHydration(int hydration)
    {
        slider.value = hydration;

    }
}
