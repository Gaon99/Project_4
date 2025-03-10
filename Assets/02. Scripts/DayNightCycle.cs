using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0, 1)] public float time;
    
    public float fullDayTime;
    public float startTime = 0.4f;
    private float timeRate;
    
    public Vector3 noon;

    [Header("Sun")] public Light sun;
    public Gradient SunGradient;
    public AnimationCurve SunIntensity;
    
    [Header("Moon")] public Light moon;
    public Gradient MoonGradient;
    public AnimationCurve MoonIntensity;
    
    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    void Start()
    {
        timeRate = 1f/fullDayTime;
        time = startTime;
    }

    void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1f;
        UpdateLighting(sun,SunGradient,SunIntensity);
        UpdateLighting(moon,MoonGradient,MoonIntensity);
        
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightsource, Gradient gradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);
        lightsource.transform.eulerAngles = (time - (lightsource == sun ? 0.25f : 0.75f)) * noon * 4f;
        lightsource.color = gradient.Evaluate(time);
        lightsource.intensity = intensity;

        GameObject go = lightsource.gameObject;

        if (lightsource.intensity == 0&&go.activeInHierarchy)
        {
            go.SetActive(false);
        }
        else if (lightsource.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}
