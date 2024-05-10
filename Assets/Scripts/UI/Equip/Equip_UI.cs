using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equip_UI : MonoBehaviour
{
    Equip equip;

    PlayerInput inputActions;

    public Equip Equip => equip;

    DragSlotUI dragSlot;

    EquipSlot_UI[] equipSlot_UI;

    [SerializeField] DropSlotUI dropSlot;

    [SerializeField] InventoryManager invenManager;

    [SerializeField] RectTransform invenTransform;

    [SerializeField] CanvasGroup canvas;

    QuickSlot quickSlot;

    public QuickSlot QuickSlot => quickSlot;
     
    public ItemData data01;
    public ItemData data02;
    public ItemData data03;
    public ItemData data04;
    public ItemData data05;


    Player Owner => equip.Owner;




    private void Awake()
    {
        inputActions = new PlayerInput();

        Transform child = transform.GetChild(0);
        equipSlot_UI = child.GetComponentsInChildren<EquipSlot_UI>();

        child = transform.GetChild(1);
        dropSlot = child.GetComponent< DropSlotUI>();

        invenManager = GetComponentInParent<InventoryManager>();

        invenTransform = GetComponent<RectTransform>();

        canvas = GetComponent<CanvasGroup>();

        dragSlot = GetComponentInChildren<DragSlotUI>();

        quickSlot = GetComponent<QuickSlot>();
    }

    private void OnEnable()
    {
    }



    void OnDisable()
    {
    }

    public void InitializeInventory(Equip playerEquip)
    {
        equip = playerEquip;

        for (uint i = 0; i < equipSlot_UI.Length; i++)
        {
            equipSlot_UI[i].InitializeSlot(equip[i]);
            equipSlot_UI[i].onDragBegin += OnItemMoveBegin;
            equipSlot_UI[i].onDragEnd += OnItemMoveEnd;
            equipSlot_UI[i].OnClick += OnClick;
        }
        invenManager.DragSlot.InitializeSlot(equip.DragSlot);  // �ӽ� ���� �ʱ�ȭ

        dropSlot.Close();

    }

    private void OnItemMoveBegin(ItemSlot slot)
    {
        invenManager.DragSlot.InitializeSlot(equip.DragSlot);  // �ӽ� ���� �ʱ�ȭ
        equip.MoveItem(slot, invenManager.DragSlot.ItemSlot);
        invenManager.DragSlot.Open();
    }



    /// <summary>
    /// ������ �巡�װ� ���̳��� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="index">���� ������ index</param>
    private void OnItemMoveEnd(ItemSlot slot, RectTransform rect)
    {
        equip.MoveItem(invenManager.DragSlot.ItemSlot, slot);

        Inventory_UI inven;
        inven = FindObjectOfType<Inventory_UI>();

        if (inven != null)
        {
            //inven.MinusValue(slot, (int)slot.ItemCount);
            //inven.PlusValue(slot);
        }

        if (invenManager.DragSlot.ItemSlot.IsEmpty)
        {
            invenManager.DragSlot.Close();
        }

    }

    /// <summary>
    /// ������ Ŭ���ϸ� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="index"></param>
    private void OnClick(ItemSlot slot, RectTransform rect)
    {
        if (!invenManager.DragSlot.ItemSlot.IsEmpty)
        {
            OnItemMoveEnd(slot, rect);
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

    private void InventoryOnOff(UnityEngine.InputSystem.InputAction.CallbackContext context)
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
}
