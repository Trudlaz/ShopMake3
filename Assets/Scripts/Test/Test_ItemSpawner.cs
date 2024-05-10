using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_ItemSpawner : TestBase
{
    public ItemSpawner itemSpawner;

#if UNITY_EDITOR
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        itemSpawner.Test_Spawn();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        itemSpawner.Test_Clear();
    }
#endif
}
