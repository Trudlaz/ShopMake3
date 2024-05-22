using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuickSlot : MonoBehaviour
{
    private PlayerInput playerInput;
    private Player owner;
    public Player Owner => owner;

    PlayerInput UIinputActions;

    public Action<Equipment> onWeaponChange;
    public Action<Equipment> onGranadeChange;
    public Action<Equipment> onETCChange;

    private void Awake()
    {
        UIinputActions = new PlayerInput();
    }

    private void OnEnable()
    {
        UIinputActions.UI.Enable();
        UIinputActions.UI.QuickSlot1.performed += MainWeapon1;
        UIinputActions.UI.QuickSlot2.performed += ThrowWeapon;
        UIinputActions.UI.QuickSlot3.performed += ETCSlot;
    }

    private void OnDisable()
    {
        UIinputActions.UI.QuickSlot3.performed -= ETCSlot;
        UIinputActions.UI.QuickSlot2.performed -= ThrowWeapon;
        UIinputActions.UI.QuickSlot1.performed -= MainWeapon1;
        UIinputActions.UI.Disable();
    }

    private void MainWeapon1(InputAction.CallbackContext context)
    {
        onWeaponChange?.Invoke(Equipment.Gun);
    }

    private void ThrowWeapon(InputAction.CallbackContext context)
    {
        onGranadeChange?.Invoke(Equipment.Throw);
    }

    private void ETCSlot(InputAction.CallbackContext context)
    {
        onETCChange?.Invoke(Equipment.ETC);
    }
}
