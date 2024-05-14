using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test_Time : MonoBehaviour
{
    float timer = 0;
    TextMeshPro counter;

    private void Awake()
    {
        counter = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        counter.text = timer.ToString();
    }
}
