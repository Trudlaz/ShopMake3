using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_InGameScene : TestBase
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
#if UNITY_EDITOR
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        gameManager.Test_GameLoad();
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        gameManager.Test_GameEnd();
    }
#endif
}
