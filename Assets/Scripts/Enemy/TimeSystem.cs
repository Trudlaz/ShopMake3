using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public float nightTime = 1800.0f; //30Ка
    public float playTime = 2400.0f; //40Ка
    public float amplif = 60;
    public Action<float> onNightchange;

    bool isNight => currentTime > nightTime;
    float nightAmpl = 1.0f;
    float currentTime;
    float nightTimeRange = 0.0f;
    float playTimeCal;
    Vector3 lightAngle = Vector3.zero;

    Action timeSys;

    GameObject dirLight;
    private void Awake()
    {
        dirLight = transform.GetChild(0).gameObject;
        playTimeCal = 1/ playTime;
    }

    private void Start()
    {
        timeSys = TimerStop;
        TimerReset();
        GameManager.Instance.OnGameStartCompleted += GameStart;
        GameManager.Instance.OnGameEnding += GameEnd;
    }

    private void Update()
    {
        timeSys();
    }

    void TimerStart() 
    {
        currentTime += Time.deltaTime;

        lightAngle.x = 90.0f + (currentTime * playTimeCal * 180.0f);
        dirLight.transform.rotation = Quaternion.Euler(lightAngle);
        if (isNight)
        {
            nightTimeRange += Time.deltaTime;
            nightAmpl = 1 + (nightTimeRange / amplif);
            onNightchange?.Invoke(nightAmpl);
        }
    }

    void TimerStop()
    {
    }

    void TimerReset()
    {
        currentTime = 0.0f;
        nightAmpl = 1.0f;
        lightAngle = Vector3.zero;
        onNightchange?.Invoke(nightAmpl);
        dirLight.transform.rotation = Quaternion.Euler(lightAngle);
    }

    private void GameStart()
    {
        TimerReset();
        timeSys = TimerStart;
    }

    private void GameEnd()
    {
        timeSys = TimerStop;
    }

#if UNITY_EDITOR
    public void Test_TimeGo()
    {
        GameStart();
    }
    public void Test_TimeEnd()
    {
        GameEnd();
    }
#endif
}
