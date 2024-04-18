using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBase : ItemBase
{
    public float amountBuff = 10.0f;
    public float Maxduration = 10.0f;

    protected Player player;

    private void Awake()
    {
        player = GameManager.Instance.Player;
    }

    public override void Use()
    {
        
    }
}
