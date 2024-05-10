using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test_Frame : MonoBehaviour
{
    float flameTimer = 0;
    int frame = 0;
    TextMeshPro counter;

    private void Awake()
    {
        counter = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        frame++;
        flameTimer += Time.deltaTime;

        if(flameTimer > 1.0f) 
        {
            counter.text = frame.ToString();
            frame = 0;
            flameTimer = 0.0f;
        }
    }
}
