using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : ArmorBase
{
    public override void Use()
    {
         player.defense +=amountDefense; // 장착하면 방어력이 증가하는 코드?
    }

    //public override void UnUse()
    //{
    //    player.defense -=amountDefense; // 해제하면 방어력이 줄어드는 코드?
    //}
}
