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

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        GameManager.Instance.TimeSystem.Test_TimeGo();
    }
    protected override void OnTest4(InputAction.CallbackContext context)
    {
        GameManager.Instance.TimeSystem.Test_TimeEnd();
    }
#endif
}
