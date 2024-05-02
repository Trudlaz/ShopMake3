using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.SceneManagement;
using UnityEngine;

//public class Equip : Inventory
public class Equip : MonoBehaviour
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

    private const int Default_Inventory_Size = 8;
    public EquipSlot[] slots;
    public EquipSlot this[uint index] => slots[index];
    private int SlotCount => slots.Length;
    private Player owner;
    private DragSlot dragSlot;
    private uint dragSlotIndex = 999999999;
    public DragSlot DragSlot => dragSlot;
    ItemDataManager itemDataManager;
    public Player Owner => owner;
    private Equip_UI equipUI;
    // private SlotType slotsType;

    //public Equip(Player owner, uint size = Default_Inventory_Size)
    //    : base (owner, size)
    //{

    //}

    public Equip(Player owner, GameObject slotsParent, uint size = Default_Inventory_Size)
    {
        slots = new EquipSlot[size];

        for (uint i = 0; i < slots.Length; i++)
        {
            slots[i] = new EquipSlot(i);
            slots[i].slotType = slotsParent.GetComponentsInChildren<EquipSlot_UI>()[i].slotType;
        }

        dragSlot = new DragSlot(dragSlotIndex);
        itemDataManager = GameManager.Instance.ItemData;
        equipUI = GameManager.Instance.EquipUI;
        this.owner = owner;
    }

    private void Awake()
    {
        //slots[0].AssignSlotItem(equipUI.data01);
        //MoveItem(slots[1], slots[0]);
        //slots[0].AssignSlotItem(equipUI.data02);
        //slots[0].AssignSlotItem(equipUI.data03);
    }

    /// <summary>
    /// 정비창에 특정 아이템을 장착하는 함수.
    /// </summary>
    /// <param name="code">장착 할 아이템 코드</param>
    /// <returns>성공하면 true 리턴 실패하면 false 리턴</returns>
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
        bool result = false;

        if (IsValidIndex(slotIndex))
        {
            ItemData data = itemDataManager[code];
            EquipSlot slot = slots[slotIndex];

            if (slot.slotType.Contains(data.itemType))
            {
                if (slot.slotType.Contains(ItemType.Gun))
                {
                    Pistol pistol = gameObject.AddComponent<Pistol>();
                    //Rifle rifle = gameObject.AddComponent<Rifle>();
                    //Shotgun shotgun = gameObject.AddComponent<Shotgun>();
                    //Sniper sniper = gameObject.AddComponent<Sniper>();
                    var slotItemComponents = slot.ItemData.itemPrefab.GetComponents<Component>();

                    foreach (var gunCom in slotItemComponents)
                    {
                        if (gunCom.GetType() == pistol.GetType())
                        {
                            //slotIndex = 2;
                            //slot = slots[slotIndex];
                            if (slotIndex != 2)
                            {
                                result = false;

                                return result;
                            }
                        }
                        else
                        {

                        }
                    }
                }

                //if (slot.slotType.Contains(data.itemType) && slot.IsEmpty)
                if (slot.IsEmpty)
                {
                    //if (slot.slotType.Contains(ItemType.Gun))
                    //{
                    //    Pistol pistol = gameObject.AddComponent<Pistol>();
                    //    //Rifle rifle = gameObject.AddComponent<Rifle>();
                    //    //Shotgun shotgun = gameObject.AddComponent<Shotgun>();
                    //    //Sniper sniper = gameObject.AddComponent<Sniper>();
                    //    var slotItemComponents = slot.ItemData.itemPrefab.GetComponents<Component>();

                    //    foreach (var gunCom in slotItemComponents)
                    //    {
                    //        if (gunCom.GetType() == pistol.GetType())
                    //        {
                    //            slotIndex = 2;
                    //            slot = slots[slotIndex];
                    //        }
                    //        else
                    //        {

                    //        }
                    //    }
                    //}

                    slot.AssignSlotItem(data);
                    result = true;
                }   
            }
        else
        {
            if (slot.ItemData == data)
            {
                // result = slot.SetSlotCount(out _);  아이템 증가 필요 없음.
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

    public void MoveItem(ItemSlot from, ItemSlot to)
    {
        // from 지점과 to지점은 서로 다른 위치이고 모두 valid한 슬롯이어야 한다.
        if ((from != to) && IsValidIndex(from) && IsValidIndex(to))
        {
            bool fromIsTemp = (from == dragSlot);
            ItemSlot fromSlot = fromIsTemp ? DragSlot : from;   // from슬롯 가져오기

            if (!fromSlot.IsEmpty)
            {
                // from에 아이템이 있다.
                ItemSlot toSlot = null;

                if (to == dragSlot)
                {
                    // to가 drag슬롯이면
                    toSlot = DragSlot;      // 슬롯 저장하고
                    DragSlot.SetFromIndex(fromSlot.Index);  // 드래고 시작 슬롯 저장(fromIndex)
                }
                else
                {
                    toSlot = to;        // drag슬롯이 아니면 슬롯만 저장
                }

                if (fromSlot.ItemData == toSlot.ItemData)
                {
                    //// 같은 종류의 아이템 => to에 채울 수 있는데까지 채운다. to에 넘어간 만큼 from을 감소시킨다.
                    //toSlot.SetSlotCount(out uint overCount, fromSlot.ItemCount);    // from이 가진 개수만큼 to 추가
                    //fromSlot.DecreaseSlotItem(fromSlot.ItemCount - overCount);      // from에서 to로 넘어간 개수만큼만 감소

                    // 같은 아이템은 추가 안한다.
                }
                else
                {
                    if (fromIsTemp)
                    {
                        ItemSlot returnSlot = slots[DragSlot.FromIndex];

                        if (returnSlot.IsEmpty)
                        {
                            returnSlot.AssignSlotItem(toSlot.ItemData, toSlot.ItemCount);
                            toSlot.AssignSlotItem(DragSlot.ItemData, DragSlot.ItemCount);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slotA"></param>
    /// <param name="slotB"></param>
    public void SwapSlot(ItemSlot slotA, ItemSlot slotB)
    {
        ItemData dragData = slotA.ItemData;
        uint dragCount = slotA.ItemCount;

        slotA.AssignSlotItem(slotB.ItemData, slotB.ItemCount);
        slotB.AssignSlotItem(dragData, dragCount);
    }

    private bool IsValidIndex(uint index)
    {
        return (index < SlotCount) || (index == dragSlotIndex);
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
    public void RemoveItem(uint slotIndex, uint count = 1)
    {
        if (IsValidIndex(slotIndex))
        {
            EquipSlot slot = slots[slotIndex];
            slot.DecreaseSlotItem(count);
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

    /// <summary>
    /// 플레이어가 사망하거나 중간에 나갔을 때 인벤토리를 비우는 함수
    /// </summary>
    public void GameOver()
    {
        ClearEquip();
    }
}