using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public float nightTime = 1800.0f; //30Ка
    public float amplif = 60;
    public Action<float> onNightchange;

    bool isNight => currentTime > nightTime;
    float nightAmpl = 1.0f;
    float currentTime;
    float nightTimeRange = 0.0f;

    private void OnEnable()
    {
        currentTime = 0.0f;
        nightAmpl = 1.0f;
        onNightchange?.Invoke(nightAmpl);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (isNight) 
        {
            nightTimeRange += Time.deltaTime;
            nightAmpl = 1 + (nightTimeRange / amplif);
            onNightchange?.Invoke(nightAmpl);
        }
    }
}
