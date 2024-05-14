using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : BuffBase
{

    public override void Use()
    {
        player.limitWeight += amountBuff;
        player.MaxWeight += amountBuff;

        StartCoroutine(Duration());
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(Maxduration);

        player.limitWeight -= amountBuff;
        player.MaxWeight -= amountBuff;
    }
}
