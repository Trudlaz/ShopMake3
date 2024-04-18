using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase : ItemBase
{
    public float amount = 10.0f;

    private void OnTriggerEnter(Collider other)
    {
        Use(other.gameObject);
    }

    protected virtual void Use(GameObject target)
    {

    }
}
