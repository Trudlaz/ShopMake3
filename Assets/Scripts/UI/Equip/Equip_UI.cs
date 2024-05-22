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

    public Equip Equip => equip;


    EquipSlot_UI[] equipSlot_UI;

    DropSlotUI dropSlot;

    InventoryManager invenManager;

    RectTransform invenTransform;

    CanvasGroup canvas;

    Inventory_UI inven;

    Player Owner => equip.Owner;

    Transform weaponTransform;

    QuickSlot QuickSlot;

    private void Awake()
    {
        

        Transform child = transform.GetChild(0);
        equipSlot_UI = child.GetComponentsInChildren<EquipSlot_UI>();

        child = transform.GetChild(1);
        dropSlot = child.GetComponent< DropSlotUI>();

        invenManager = GetComponentInParent<InventoryManager>();

        invenTransform = GetComponent<RectTransform>();

        canvas = GetComponent<CanvasGroup>();

        QuickSlot = GetComponent<QuickSlot>();
    }


    public void InitializeInventory(Equip playerEquip)
    {
        equip = playerEquip;

        for (uint i = 0; i < equipSlot_UI.Length; i++)
        {
            equipSlot_UI[i].InitializeSlot(equip[i]);

            QuickSlot.onWeaponChange = Equipment;
            QuickSlot.onGranadeChange = Equipment;
            QuickSlot.onETCChange = Equipment;
        }
        invenManager.DragSlot.InitializeSlot(equip.DragSlot);  // �ӽ� ���� �ʱ�ȭ

        inven = GameManager.Instance.InventoryUI;
        dropSlot.Close();

        Close();
    }

    void Equipment(Equipment type)
    {
        Owner.Equipped(type);  
    }

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
        if(inven.Inventory.RemoveItem(equipSlot_UI[index].ItemSlot.ItemData.itemId) < 1)
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
}
