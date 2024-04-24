using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInventory
{
    // 기본 인벤토리 크기 설정
    const int Default_Inventory_Size = 144;

    // 아이템 슬롯 배열
    ItemSlot[] slots;

    // 인덱스 접근을 위한 인덱서
    public ItemSlot this[uint index] => slots[index];
    // 슬롯 개수 반환
    int SlotCount => slots.Length;

    // 드래그 중인 아이템 슬롯
    DragSlot dragSlot;
    // 드래그 슬롯 인덱스 초기값
    uint dragSlotIndex = 999999999;
    // 드래그 슬롯 접근 프로퍼티
    public DragSlot DragSlot => dragSlot;

    // 아이템 데이터 관리자
    ItemDataManager itemDataManager;

    // 인벤토리 소유자
    Player owner;
    // 소유자 접근 프로퍼티
    public Player Owner => owner;

    // 월드 인벤토리 UI 접근
    WorldInventory_UI worldInven;

    // 아이템 개수 감소 시 호출되는 이벤트
    public Action<ItemSlot> onMinusValue;
    // 아이템 개수 증가 시 호출되는 이벤트
    public Action<ItemSlot> onPlusValue;

    // 생성자
    public WorldInventory(Player owner = null, uint size = Default_Inventory_Size)
    {
        slots = new ItemSlot[size];
        for (uint i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot(i);
        }
        dragSlot = new DragSlot(dragSlotIndex);
        itemDataManager = GameManager.Instance.ItemData;
        worldInven = GameManager.Instance.WorldInventory_UI;
        this.owner = owner;
    }

    // 아이템 추가 메서드
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

    // 특정 슬롯에 아이템 추가 메서드
    public bool AddItem(ItemCode code, uint slotIndex)
    {
        bool result = false;

        if (IsValidIndex(slotIndex))
        {
            ItemData data = itemDataManager[code];
            ItemSlot slot = slots[slotIndex];
            if (slot.IsEmpty)
            {
                slot.AssignSlotItem(data);
                PlusMoney(slot);
                result = true;
            }
            else
            {
                if (slot.ItemData == data)
                {
                    PlusMoney(slot);
                    result = slot.SetSlotCount(out _);
                }
            }
        }
        return result;
    }

    // 아이템 제거 메서드
    public void RemoveItem(uint slotIndex, uint count = 1)
    {
        if (IsValidIndex(slotIndex))
        {
            ItemSlot slot = slots[slotIndex];
            slot.DecreaseSlotItem(count);
        }
    }

    // 아이템 이동 메서드
    public void MoveItem(ItemSlot from, ItemSlot to)
    {
        if ((from != to) && IsValidIndex(from) && IsValidIndex(to))
        {
            bool fromIsTemp = (from == dragSlot);
            ItemSlot fromSlot = fromIsTemp ? DragSlot : from;

            if (!fromSlot.IsEmpty)
            {
                ItemSlot toSlot = null;

                if (to == dragSlot)
                {
                    toSlot = DragSlot;
                    DragSlot.SetFromIndex(fromSlot.Index);
                }
                else
                {
                    toSlot = to;
                }

                if (fromSlot.ItemData == toSlot.ItemData)
                {
                    toSlot.SetSlotCount(out uint overCount, fromSlot.ItemCount);
                    fromSlot.DecreaseSlotItem(fromSlot.ItemCount - overCount);
                }
                else
                {
                    if (fromIsTemp)
                    {
                        ItemSlot returnSlot = slots[DragSlot.FromIndex];

                        if (returnSlot.IsEmpty)
                        {
                            returnSlot.AssignSlotItem(toSlot.ItemData, toSlot.ItemCount, toSlot.IsEquiped);
                            toSlot.AssignSlotItem(DragSlot.ItemData, DragSlot.ItemCount, DragSlot.IsEquiped);
                            DragSlot.ClearSlot();
                        }
                        else
                        {
                            if (returnSlot.ItemData == toSlot.ItemData)
                            {
                                MoveItem(toSlot, returnSlot);
                                returnSlot.SetSlotCount(out uint overCount, toSlot.ItemCount);
                                toSlot.DecreaseSlotItem(toSlot.ItemCount - overCount);
                            }
                            SwapSlot(dragSlot, toSlot);
                        }
                    }
                    else
                    {
                        SwapSlot(fromSlot, toSlot);
                    }
                }
            }
        }
    }

    // 슬롯 간 아이템 교체 메서드
    public void SwapSlot(ItemSlot slotA, ItemSlot slotB)
    {
        ItemData dragData = slotA.ItemData;
        uint dragCount = slotA.ItemCount;
        bool dragEquiped = slotA.IsEquiped;

        slotA.AssignSlotItem(slotB.ItemData, slotB.ItemCount, slotB.IsEquiped);
        slotB.AssignSlotItem(dragData, dragCount, dragEquiped);
    }

    // 슬롯 정렬 메서드
    public void SlotSorting(ItemType type, bool isAcending)
    {
        List<ItemSlot> temp = new List<ItemSlot>(slots);
        switch (type)
        {
            case ItemType.Buff:
                temp.Sort((current, other) =>
                {
                    if (current.ItemData == null)
                        return 1;
                    if (other.ItemData == null)
                        return -1;
                    if (isAcending)
                    {
                        return current.ItemData.itemType.CompareTo(other.ItemData.itemType);
                    }
                    else
                    {
                        return other.ItemData.itemType.CompareTo(current.ItemData.itemType);
                    }
                });
                break;
        }
        List<(ItemData, uint, bool)> sortedData = new List<(ItemData, uint, bool)>(SlotCount);
        foreach (var slot in temp)
        {
            sortedData.Add((slot.ItemData, slot.ItemCount, slot.IsEquiped));
        }

        int index = 0;
        foreach (var data in sortedData)
        {
            slots[index].AssignSlotItem(data.Item1, data.Item2, data.Item3);
            index++;
        }
    }

    // 슬롯 초기화 메서드
    public void ClearSlot(uint slotIndex)
    {
        if (IsValidIndex(slotIndex))
        {
            ItemSlot slot = slots[slotIndex];
            slot.ClearSlot();
        }
    }

    // 전체 인벤토리 초기화 메서드
    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
    }

    // 유효한 인덱스 검사 메서드
    bool IsValidIndex(uint index)
    {
        return (index < SlotCount) || (index == dragSlotIndex);
    }

    // 유효한 슬롯 검사 메서드
    bool IsValidIndex(ItemSlot slot)
    {
        return (slot != null) || (slot == dragSlot);
    }

    // 아이템 병합 메서드
    public void MergeItems()
    {
        uint count = (uint)slots.Length - 1;
        for (uint i = 0; i < count; i++)
        {
            ItemSlot target = slots[i];
            for (uint j = count - 1; j > i; j--)
            {
                if (target.ItemData == slots[j].ItemData)
                {
                    MoveItem(slots[j], target);

                    if (!slots[j].IsEmpty)
                    {
                        SwapSlot(slots[i + 1], slots[j]);
                        break;
                    }
                }
            }
        }
    }

    // 특정 아이템 찾기 메서드
    public bool FindItem(ItemType itemType)
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (!slots[i].IsEmpty && slots[i].ItemData.itemType == itemType)
            {
                Debug.Log("열쇠 확인");
                return true;
            }
        }
        Debug.Log("열쇠 찾지 못함");
        return false;
    }

#if UNITY_EDITOR
    // 인벤토리 상태 출력 메서드
    public void Test_InventoryPrint()
    {
        string invenInfo = "";
        string dataName = "";
        foreach (var slot in slots)
        {
            if (slot.ItemData != null)
            {
                dataName = slot.ItemData.itemType.ToString();
                invenInfo += $"{dataName}({slot.ItemCount}/{slot.ItemData.maxItemCount}) ";
            }
            else
            {
                invenInfo += "(빈칸) ";
            }
        }
        Debug.Log($"[{invenInfo}]");
    }

    // 아이템 가치에 따라 돈 증가 메서드
    public void PlusMoney(ItemSlot slot, int count = 1)
    {
        if (slot.ItemData != null)
        {
            worldInven.Money += (int)(slot.ItemData.Price * count);
        }
    }

    // 아이템 가치에 따라 돈 감소 메서드
    public void MinusMoney(ItemSlot slot, int count = 1)
    {
        if (slot.ItemData != null)
        {
            worldInven.Money -= (int)(slot.ItemData.Price * count);
        }
    }
#endif
}
