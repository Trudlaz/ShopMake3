using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Door : TestBase
{
    public DoorBase DoorCycleExit;
    public DoorBase DoorKeyExit;
    public DoorBase DoorNormal;
    public DoorBase DoorKeyNormal;
    public DoorBase DoorSpawner;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        DoorCycleExit.Interect();
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        DoorKeyExit.Interect();
    }
    protected override void OnTest3(InputAction.CallbackContext context)
    {
        DoorNormal.Interect();
    }
    protected override void OnTest4(InputAction.CallbackContext context)
    {
        DoorKeyNormal.Interect();
    }
    protected override void OnTest5(InputAction.CallbackContext context)
    {
        DoorSpawner.Interect();
    }

}
