using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBase : ItemBase
{

    public float amountDefense = 30.0f;
    protected Player player;

    private void Awake()
    {
        player = GameManager.Instance.Player;
    }

    public override void Use()
    {

    }

    public override void UnUse()
    {

    }
}
