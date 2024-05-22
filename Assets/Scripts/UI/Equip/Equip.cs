using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.SceneManagement;
using UnityEngine;



public class Equip
{
    private const int Default_Inventory_Size = 6;
    public EquipSlot[] slots;
    public EquipSlot this[uint index] => slots[index];
    private int SlotCount => slots.Length;
    private Player owner;
    private DragSlot dragSlot;
    private uint dragSlotIndex = 999999999;
    public DragSlot DragSlot => dragSlot;
    private ItemDataManager itemDataManager;
    public Player Owner => owner;
    private Equip_UI equipUI;
    public QuickSlot quickSlot;

    public Equip(GameManager owner, uint size = Default_Inventory_Size)
    {
        slots = new EquipSlot[size];

        for (uint i = 0; i < slots.Length; i++)
        {
            slots[i] = new EquipSlot(i);
            //slots[i].PlayerEquipment = slotsParent.GetComponentsInChildren<EquipSlot_UI>()[i].PlayerEquipment;  // awake 함수로 따로 찾기
        }

        dragSlot = new DragSlot(dragSlotIndex);
        itemDataManager = GameManager.Instance.ItemData;
        equipUI = GameManager.Instance.EquipUI;
        this.owner = owner.Player;

        Transform equipUIObject = equipUI.gameObject.transform.GetChild(0);

        for (uint i = 0; i < slots.Length; i++)
        {
            slots[i].slotType = equipUIObject.GetChild((int)i).GetComponent<EquipSlot_UI>().slotType;  // awake 함수로 따로 찾기

            //slots[i].PlayerEquip = EquipSlot.PlayerEquipment[(int)i]
        }
    }

    /// <summary>
    /// 정비창에 특정 아이템을 장착하는 함수.
    /// </summary>
    /// <param name="code">장착 할 아이템 코드</param>
    ///// <returns>성공하면 true 리턴 실패하면 false 리턴</returns>
    public bool AddItem(ItemCode code)
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (AddItem(code, (uint)i))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 특정 정비창 슬롯에 특정 아이템을 1개 장착하는 함수
    /// </summary>
    /// <param name="code">장착 할 아이템의 코드</param>
    /// <param name="slotIndex">아이템을 장착 할 슬롯의 인덱스</param>
    /// <returns>성공하면 true 리턴 실패하면 false 리턴</returns>
    public bool AddItem(ItemCode code, uint slotIndex)
    {

        if (IsValidIndex(slotIndex))
        {
            ItemData data = itemDataManager[code];
            EquipSlot slot = slots[slotIndex];

            if (slot.slotType.Contains(data.itemType))
            {
                if (slot.IsEmpty)
                {
                    slot.AssignSlotItem(data);
                    return true;
                }
            }
        }
        return false;
    }


    public bool IsValidIndex(uint index)
    {
        return index < SlotCount;
    }

    private bool IsValidIndex(ItemSlot slot)
    {
        return (slot != null) || (slot == dragSlot);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slotIndex"></param>
    /// <param name="count"></param>
    private void RemoveItem(uint slotIndex, uint count = 1)
    {
        if (IsValidIndex(slotIndex))
        {
            EquipSlot slot = slots[slotIndex];
            slot.DecreaseSlotItem(count);
        }
    }

    public void RemoveItem(ItemCode code,  uint count = 1)
    {
        for(int i = 0; i < SlotCount; i++)
        {
            ItemData data = itemDataManager[code];
            EquipSlot slot = slots[i];

            if (slot.slotType.Contains(data.itemType))
            {
                if (!slot.IsEmpty)
                {
                    RemoveItem((uint)i, count);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ClearEquip()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
    }

}