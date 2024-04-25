using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Equip : Inventory
{
    //[SerializeField] public Slot[] slots;
    //private QuickSlotUI quickSlotUI;

    //private void Awake()
    //{
    //    slots = GetComponentsInChildren<Slot>();
    //    quickSlotUI = FindObjectOfType<QuickSlotUI>(true);
    //}

    ///// <summary>
    ///// 슬롯에 아이템 정보 추가.
    ///// </summary>
    ///// <param name="item"></param>
    ///// <param name="index"></param>
    //public void AddItemToSlot(ItemData item, uint index)
    //{
    //    if (slots[index].IsEmpty)
    //    {
    //        slots[index].AddItem(item);
    //        quickSlotUI.QuickSlotImageChange(index);
    //    }
    //}

    ///// <summary>
    ///// 슬롯에 이미 아이템이 존재할 경우 그 아이템과 장착 아이템 변경.
    ///// </summary>
    ///// <param name="from"></param>
    ///// <param name="to"></param>
    //public void SwitchItemToSlot(Slot from, Slot to)
    //{
    //    if (!from.IsEmpty && to.IsEmpty)
    //    {
    //        ItemData temp = from.itemData;
    //        from.RemoveItem();
    //        to.EquipItem(temp);
    //    }
    //}

    public ItemSlot[] slots;
    private SlotType slotsType;
    const int Default_Inventory_Size = 8;
    //Player owner;

    public Equip(Player owner, uint size = Default_Inventory_Size)
        : base (owner, size)
    {

    }

    /// <summary>
    /// 장착할 아이템의 타입과 슬롯 타입을 비교 후 결과에 따라 참/거짓 리턴
    /// </summary>
    /// <param name="data">장착할 아이템의 데이터</param>
    /// <returns></returns>
    public bool CheckItemSlotType(ItemData data)
    {
        bool isEquip = false;

        if (data != null && slotsType.slotType.Contains(data.itemType))
        {
            isEquip = true;
        }

        return isEquip;
    }

    //public void EquipItem(ItemCode code, ItemData data)
    //{
    //    if (data != null && slotsType.slotType.Contains(data.itemType))
    //    {
    //        base.AddItem(code);
    //    }
    //}
}