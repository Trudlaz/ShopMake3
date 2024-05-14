using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : BuffBase
{
    public override void Use()
    {
        player.moveSpeed += amountBuff;

        StartCoroutine(Duration());
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(Maxduration);

        player.moveSpeed -= amountBuff;
    }


   
}
