using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : BuffBase
{
    public override void Use()
    {
        if (player != null)
        {
            player.limitWeight += amountBuff;
            player.MaxWeight += amountBuff;

            StartCoroutine(Duration());
            base.Use();
        }
        else
        {
            Debug.LogError("Player is not assigned when trying to use Heal.");
        }
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(Maxduration);

        player.limitWeight -= amountBuff;
        player.MaxWeight -= amountBuff;
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    public void Test_Use()
    {
        Use();
    }
#endif
}
