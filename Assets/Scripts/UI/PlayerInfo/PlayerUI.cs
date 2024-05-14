using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    RemainBullet remain;

    public RemainBullet Remain => remain;

    private void Awake()
    {
        remain = GetComponentInChildren<RemainBullet>();
    }


    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
