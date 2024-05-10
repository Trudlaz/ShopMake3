using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vest : ArmorBase
{
    public override void Use()
    {
        player.defense += amountDefense; // 장착하면 방어력이 증가하는 코드?
    }

    public override void UnUse()
    {
        player.defense -= amountDefense; // 해제하면 방어력이 줄어드는 코드?
    }

    // 방어력이 있을 때 적에게 피격당할시 방어력 깎이는 코드?
    //public void TakeDamege(float damege)
    //{
    //    if (currentDefense > 0)
    //    {
    //        // 방어력이 있는 경우
    //        int remainingDamage = damage - currentDefense;
    //        if (remainingDamage < 0)
    //        {
    //            // 방어력이 데미지보다 더 큰 경우
    //            currentDefense -= damage;
    //        }
    //        else
    //        {
    //            // 방어력이 데미지보다 작거나 같은 경우
    //            currentDefense = 0;
    //            currentHealth -= remainingDamage;
    //        }
    //    }
    //    else
    //    {
    //        // 방어력이 없는 경우
    //        currentHealth -= damage;
    //    }
    //}
}
