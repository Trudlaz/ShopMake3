using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : WeaponBase
{


    public override void Use()
    {
        base.Use();         // WeaponBase에서 구현했던 기능 그대로 사용
    }
}
