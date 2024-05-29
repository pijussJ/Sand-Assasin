using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandstorm : MonoBehaviour
{
    public static bool isSandstorm;

    public float sandstormDelaySeconds;
    public float fogDensity = 0.1f;

    private void Start()
    {
        RenderSettings.fog = false;
        Invoke("StartSandstorm", sandstormDelaySeconds);
    }

    private void Update()
    {
        if (RenderSettings.fog && RenderSettings.fogDensity < fogDensity)
        {
            RenderSettings.fogDensity += (fogDensity - RenderSettings.fogDensity) * 0.01f;
        }
    }

    void StartSandstorm()
    {
        isSandstorm = true;
        GetComponent<AudioSource>().Play();
        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0;
    }
}
