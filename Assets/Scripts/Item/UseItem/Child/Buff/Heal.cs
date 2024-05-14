using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BuffBase
{


    public override void Use()
    {
        player.Heal(amountBuff);
    }

}
