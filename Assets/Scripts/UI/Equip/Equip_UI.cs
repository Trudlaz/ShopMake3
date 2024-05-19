using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Equipment : byte
{
    None = 0,
    Main1,
    Main2,
    Sub,
    Armor,
    Helmet,
    Throw,
    BackPack,
    ETC
}


public class Equip_UI : MonoBehaviour
{
    Equip equip;



    public Equip Equip => equip;


    EquipSlot_UI[] equipSlot_UI;

    DropSlotUI dropSlot;

    InventoryManager invenManager;

    RectTransform invenTransform;

    CanvasGroup canvas;

    QuickSlot quickSlot;

    Inventory_UI inven;

    public QuickSlot QuickSlot => quickSlot;

    public ItemData data01;
    public ItemData data02;
    public ItemData data03;
    public ItemData data04;
    public ItemData data05;

    Player Owner => equip.Owner;

    Transform weaponTransform;

    PlayerInput UIinputActions;

    Equipment equipment = Equipment.None;

    public Equipment Equipment
    {
        get => equipment;
        set
        {
            if (equipment != value)
            {
                equipment = value;
            }
        }
    }


    private void Awake()
    {
        UIinputActions = new PlayerInput();

        Transform child = transform.GetChild(0);
        equipSlot_UI = child.GetComponentsInChildren<EquipSlot_UI>();

        child = transform.GetChild(1);
        dropSlot = child.GetComponent<DropSlotUI>();

        invenManager = GetComponentInParent<InventoryManager>();

        invenTransform = GetComponent<RectTransform>();

        canvas = GetComponent<CanvasGroup>();

        quickSlot = GetComponent<QuickSlot>();
    }

    private void OnEnable()
    {
        // UI 관련
        UIinputActions.UI.Enable();
        UIinputActions.UI.QuickSlot1.performed += MainWeapon1;
        UIinputActions.UI.QuickSlot2.performed += MainWeapon2;
        UIinputActions.UI.QuickSlot3.performed += SubWeapon;
        UIinputActions.UI.QuickSlot4.performed += ThrowWeapon;
        UIinputActions.UI.QuickSlot5.performed += ETCSlot;
    }

    void OnDisable()
    {
        UIinputActions.UI.QuickSlot5.performed -= ETCSlot;
        UIinputActions.UI.QuickSlot4.performed -= ThrowWeapon;
        UIinputActions.UI.QuickSlot3.performed -= SubWeapon;
        UIinputActions.UI.QuickSlot2.performed -= MainWeapon2;
        UIinputActions.UI.QuickSlot1.performed -= MainWeapon1;
    }



    public void InitializeInventory(Equip playerEquip)
    {
        equip = playerEquip;

        for (uint i = 0; i < equipSlot_UI.Length; i++)
        {
            equipSlot_UI[i].InitializeSlot(equip[i]);
            //equipSlot_UI[i].onDragBegin += OnItemMoveBegin;
            //equipSlot_UI[i].onDragEnd += OnItemMoveEnd;
            //equipSlot_UI[i].OnClick += OnClick;
        }
        invenManager.DragSlot.InitializeSlot(equip.DragSlot);  // �ӽ� ���� �ʱ�ȭ
        inven = GameManager.Instance.InventoryUI;
        dropSlot.Close();

        Close();
    }

    //private void OnItemMoveBegin(ItemSlot slot)
    //{
    //    invenManager.DragSlot.InitializeSlot(equip.DragSlot);  // �ӽ� ���� �ʱ�ȭ
    //    equip.MoveItem(slot, invenManager.DragSlot.ItemSlot);
    //    invenManager.DragSlot.Open();
    //}



    ///// <summary>
    ///// ������ �巡�װ� ���̳��� ����Ǵ� �Լ�
    ///// </summary>
    ///// <param name="index">���� ������ index</param>
    //private void OnItemMoveEnd(ItemSlot slot, RectTransform rect)
    //{
    //    equip.MoveItem(invenManager.DragSlot.ItemSlot, slot);

    //    //Inventory_UI inven;
    //    //inven = rect.GetComponentInParent<Inventory_UI>();

    //    //if (inven != null)
    //    //{
    //    //    inven.MinusValue(slot, (int)slot.ItemCount);
    //    //    inven.PlusValue(slot);
    //    //}

    //    if (invenManager.DragSlot.ItemSlot.IsEmpty)
    //    {
    //        invenManager.DragSlot.Close();
    //    }

    //}

    ///// <summary>
    ///// ������ Ŭ���ϸ� ����Ǵ� �Լ�
    ///// </summary>
    ///// <param name="index"></param>
    //private void OnClick(ItemSlot slot, RectTransform rect)
    //{
    //    if (!invenManager.DragSlot.ItemSlot.IsEmpty)
    //    {
    //        OnItemMoveEnd(slot, rect);
    //    }
    //}

    public bool EquipItem(ItemSlot slot)
    {
        return equip.AddItem(slot.ItemData.itemId);
    }

    public void UnEquipItem(ItemSlot slot)
    {
        equip.RemoveItem(slot.ItemData.itemId);
    }

    public void UseItem(uint index)
    {
        if (inven.Inventory.RemoveItem(equipSlot_UI[index].ItemSlot.ItemData.itemId) < 1)
        {
            equip.RemoveItem(equipSlot_UI[index].ItemSlot.ItemData.itemId);
        }
    }

    public void open()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public void Close()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    public void InventoryOnOff()
    {
        if (canvas.interactable)
        {
            Close();
        }
        else
        {
            open();
        }
    }

    private void MainWeapon1(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Equipment = Equipment.Main1;
    }
    private void MainWeapon2(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Equipment = Equipment.Main2;
    }

    private void SubWeapon(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Equipment = Equipment.Sub;
    }

    private void ThrowWeapon(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Equipment = Equipment.Throw;
    }

    private void ETCSlot(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Equipment = Equipment.ETC;
    }
}
