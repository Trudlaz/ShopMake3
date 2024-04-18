using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapModel : MonoBehaviour
{
    public Action OnActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnActive?.Invoke();
        }
    }
}
