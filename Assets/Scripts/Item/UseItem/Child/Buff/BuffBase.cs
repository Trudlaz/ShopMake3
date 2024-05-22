using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBase : ItemBase
{
    public float amountBuff = 10.0f;
    public float Maxduration = 10.0f;

    public override void Use()
    {
        GameManager.Instance.EquipUI.UseItem(7);
    }
}
