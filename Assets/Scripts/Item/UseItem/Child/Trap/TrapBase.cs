using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase : ItemBase                        //이 아이템은 상점에서만 얻는다.
{
    public float amount = 10.0f;

    private void OnTriggerEnter(Collider other)
    {
        Use(other.gameObject);
    }

    protected virtual void Use(GameObject target)       //설치하기
    {

    }
}
