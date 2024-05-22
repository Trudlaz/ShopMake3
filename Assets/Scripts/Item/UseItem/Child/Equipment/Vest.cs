using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vest : ArmorBase
{
    public override void Use()
    {
        if (player != null)
        {
            player.defense += amountDefense;    // 장착하면 방어력이 증가하는 코드?
            Debug.Log("방어력이 증가하였습니다.");
        }
        else
        {
            Debug.LogError("Player is not assigned when trying to use Heal.");
        }
    }

    public override void UnUse()
    {
        player.defense -= amountDefense; // 해제하면 방어력이 줄어드는 코드?
    }

#if UNITY_EDITOR
    public void Test_Use()
    {
        Use();
    }
#endif
}
