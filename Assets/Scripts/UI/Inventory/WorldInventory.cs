using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInventory
{
    const int Default_Inventory_Size = 144;

    ItemSlot[] slots;

    public ItemSlot this[uint index] => slots[index];
    int SlotCount => slots.Length;

    DragSlot dragSlot;

    uint dragSlotIndex = 999999999;

    public DragSlot DragSlot => dragSlot;

    ItemDataManager itemDataManager;

    Player owner;

    public Player Owner => owner;

    WorldInventory_UI worldInven;

    public Action<ItemSlot> onMinusValue;

    public Action<ItemSlot> onPlusValue;

    public WorldInventory(Player owner, uint size = Default_Inventory_Size)
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

    /// <summary>
    /// 인벤토리에 특정아이템을 1개 추가하는 함수
    /// </summary>
    /// <param Name="code">추가할 아이템의 코드</param>
    /// <returns>true면 성공 false면 추가 실패</returns>
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
    /// 인벤토리의 특정 슬롯에 특정 아이템을 1개 추가하는 함수
    /// </summary>
    /// <param Name="code">추가할 아이템의 코드</param>
    /// <param Name="slotIndex">아이템을 추가할 슬롯의 인덱스</param>
    /// <returns>true면 성공 false면 추가 실패</returns>
    public bool AddItem(ItemCode code, uint slotIndex)
    {
        bool result = false;

        if (IsValidIndex(slotIndex))    // 인덱스가 적절한지 확인
        {
            ItemData data = itemDataManager[code];
            ItemSlot slot = slots[slotIndex];      // 슬롯 가져오기
            if (slot.IsEmpty)
            {
                // 슬롯이 비었으면
                slot.AssignSlotItem(data);          // 그대로 아이템 설정
                PlusMoney(slot);
                result = true;
            }
            else
            {
                // 슬롯이 안비어있다.
                if (slot.ItemData == data)
                {
                    // 같은 종류의 아이템이면
                    PlusMoney(slot);
                    result = slot.SetSlotCount(out _);   // 아이템 증가 시도
                }
                else
                {
                    // 다른 종류의 아이템
                }
            }
        }
        else
        {
            // 잘못된 슬롯
        }
        return result;
    }

    public void RemoveItem(uint slotIndex, uint count = 1)
    {
        if (IsValidIndex(slotIndex))
        {
            ItemSlot slot = slots[slotIndex];
            slot.DecreaseSlotItem(count);
        }
    }


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

    public void SwapSlot(ItemSlot slotA, ItemSlot slotB)
    {
        ItemData dragData = slotA.ItemData;
        uint dragCount = slotA.ItemCount;
        bool dragEquiped = slotA.IsEquiped;

        slotA.AssignSlotItem(slotB.ItemData, slotB.ItemCount, slotB.IsEquiped);
        slotB.AssignSlotItem(dragData, dragCount, dragEquiped);
    }

    public void SlotSorting(ItemType type, bool isAcending)
    {
        // 정렬을 위한 임시 리스트
        List<ItemSlot> temp = new List<ItemSlot>(slots);  // slots를 기반으로 리스트 생성

        // 정렬방법에 따라 임시 리스트 정렬
        switch (type)
        {
            case ItemType.Buff:
                temp.Sort((current, other) =>       // current, y는 temp리스트에 들어있는 요소 중 2개
                {
                    if (current.ItemData == null)   // 비어있는 슬롯을 뒤쪽으로 보내기
                        return 1;
                    if (other.ItemData == null)
                        return -1;
                    if (isAcending)                 // 오름차순/내림차순에 따라 처리  
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
        // 임시 리스트의 내용을 슬롯에 설정
        List<(ItemData, uint, bool)> sortedData = new List<(ItemData, uint, bool)>(SlotCount);  // 튜플 사용
        foreach (var slot in temp)
        {
            sortedData.Add((slot.ItemData, slot.ItemCount, slot.IsEquiped));    // 필요 데이터만 복사해서 가지기
        }

        int index = 0;
        foreach (var data in sortedData)
        {
            slots[index].AssignSlotItem(data.Item1, data.Item2, data.Item3);     // 복사한 내용을 슬롯에 설정
            index++;
        }
    }

    public void ClearSlot(uint slotIndex)
    {
        if (IsValidIndex(slotIndex))
        {
            ItemSlot slot = slots[slotIndex];
            slot.ClearSlot();
        }
    }

    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
    }

    bool IsValidIndex(uint index)
    {
        return (index < SlotCount) || (index == dragSlotIndex);
    }

    bool IsValidIndex(ItemSlot slot)
    {
        return (slot != null) || (slot == dragSlot);
    }


    public void MergeItems()
    {
        uint count = (uint)slots.Length - 1;
        for (uint i = 0; i < count; i++)
        {
            ItemSlot target = slots[i];
            for (uint j = count - 1; j > i; j--)
            {
                // 같은 종류의 아이템이면
                if (target.ItemData == slots[j].ItemData)
                {
                    MoveItem(slots[j], target);     // j에 있는 것을 i에 넣기

                    if (!slots[j].IsEmpty)
                    {
                        // 남은 아이템이 있으면 i의 다음 슬롯과 교체하기
                        SwapSlot(slots[i + 1], slots[j]);
                        break;
                    }
                }
            }
        }
    }


    /// <summary>
    /// 인벤토리 내부에서 특정 한 아이템을 찾는 함수
    /// </summary>
    /// <param name="itemType">특정한 아이템의 타입</param>
    /// <returns>true면 아이템이 인벤토리 내부에 있다, false면 없다.</returns>
    public bool FindItem(ItemType itemType)
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (!slots[i].IsEmpty && slots[i].ItemData.itemType == itemType)
            {
                Debug.Log("열쇠 확인");
                return true; // 아이템을 찾았으면 즉시 true를 반환하고 종료
            }
        }
        Debug.Log("열쇠 찾지 못함");
        return false; // 루프를 끝까지 돌았는데도 아이템을 찾지 못했으면 false 반환
    }





#if UNITY_EDITOR
    public void Test_InventoryPrint()
    {
        string invenInfo = "";
        string dataName = "";
        foreach (var slot in slots)
        {

            if (slot.ItemData != null)
            {
                if (slot.ItemData.itemType == ItemType.Buff) dataName = "버프아이템";

                else if (slot.ItemData.itemType == ItemType.Grenade) dataName = "투척무기";

                else if (slot.ItemData.itemType == ItemType.Bullet) dataName = "총기";

                else if (slot.ItemData.itemType == ItemType.Trap) dataName = "함정";

                else if (slot.ItemData.itemType == ItemType.Key) dataName = "열쇠";

                else if (slot.ItemData.itemType == ItemType.Price) dataName = "화폐";

                invenInfo += $"{dataName}({slot.ItemCount}/{slot.ItemData.maxItemCount}) ";
            }
            else
            {
                invenInfo += "(빈칸) ";
            }
        }
        Debug.Log($"[{invenInfo}]");
    }

    /// <summary>
    /// 임시로 쓰는 함수, 나중에 돈 개수로 바꾸기 위해 이곳에 배치
    /// </summary>
    /// <param name="slot"></param>
    public void PlusMoney(ItemSlot slot, int count = 1)
    {
        if (slot.ItemData != null)
        {
            //if (slot.ItemData.itemType == ItemType.Price)
            //{
                worldInven.Money += (int)(slot.ItemData.Price * count);
            //}
        }
    }

    public void MinusMoney(ItemSlot slot, int count = 1)
    {
        if (slot.ItemData != null)
        {
            //if (slot.ItemData.itemType == ItemType.Price)
            //{
                worldInven.Money -= (int)(slot.ItemData.Price * count);
            //}
        }
    }

#endif

}
