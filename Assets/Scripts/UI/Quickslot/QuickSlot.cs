using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuickSlot : MonoBehaviour
{
    private PlayerInput playerInput;
    private Player owner;
    public Player Owner => owner;

    PlayerMove UIinputActions;

    public Action<Equipment> onWeaponChange;
    public Action<Equipment> onGranadeChange;
    public Action<Equipment> onETCChange;

    private void Awake()
    {
        UIinputActions = new PlayerMove();
    }

    private void OnEnable()
    {
        UIinputActions.Player.Enable();
        UIinputActions.Player.QuickSlot1.performed += MainWeapon1;
        UIinputActions.Player.QuickSlot2.performed += ThrowWeapon;
        UIinputActions.Player.QuickSlot3.performed += ETCSlot;
    }

    private void OnDisable()
    {
        UIinputActions.Player.QuickSlot3.performed -= ETCSlot;
        UIinputActions.Player.QuickSlot2.performed -= ThrowWeapon;
        UIinputActions.Player.QuickSlot1.performed -= MainWeapon1;
        UIinputActions.Player.Disable();
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
