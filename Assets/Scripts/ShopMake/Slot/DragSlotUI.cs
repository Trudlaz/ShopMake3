using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragSlotUI : Slot_UI_Base
{
    DragSlot dragSlot;

    public uint FromIndex => dragSlot.FromIndex;



    private void Update()
    {   
        transform.position = Mouse.current.position.ReadValue();
    }

    public override void InitializeSlot(ItemSlot slot)
    {
        base.InitializeSlot(slot);
        dragSlot = slot as DragSlot;
        Close();
    }

    public void ClearSlot()
    {
        dragSlot.ClearSlot();
    }

    public void Open()
    {
        transform.position = Mouse.current.position.ReadValue();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetFromIndex(uint index)
    {
        dragSlot.SetFromIndex(index);
    }
}
