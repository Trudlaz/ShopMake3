using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : ObjectPool<EnemyBace>
{
    protected override void OnGenerateObject(EnemyBace comp)
    {
        comp.Start();
    }
}
