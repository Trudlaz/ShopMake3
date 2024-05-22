using System;
using UnityEngine;

public class ItemSlot
{
    uint slotIndex;

    public uint Index => slotIndex;


    ItemData itemData = null;   // 인벤토리에 들어가게 될 아이템 정보

    public ItemData ItemData
    {
        get => itemData;
        private set
        {
            if (itemData != value)
            {
                itemData = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    public ItemSlot(uint index)
    {
        slotIndex = index;
        itemData = null;
        itemCount = 0;
        IsEquiped = false;
    }

    public Action onSlotItemChange;

    public Action<ItemSlot> onItemEquiped;


    public bool IsEmpty => itemData == null;

    uint itemCount = 0;

    public uint ItemCount
    {
        get => itemCount;
        set
        {
            if(itemCount != value)
            {
                itemCount = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    bool isEquiped = false;

    public bool IsEquiped
    {
        get => isEquiped;
        set
        {
            isEquiped = value;
            if (isEquiped)
            {
                onItemEquiped?.Invoke(this);
            }
            onSlotItemChange?.Invoke();
        }
    }

    /// <summary>
    /// 인벤토리에 아이템을 추가해주는 함수
    /// </summary>
    /// <param name="item">획득한 아이템</param>
    /// <param name="count">획득한 아이템의 수량</param>
    public void AssignSlotItem(ItemData item, uint count = 1, bool isEquipped = false)
    {
        if(item != null)
        {
            ItemData = item;
            ItemCount = count;
            IsEquiped = isEquipped;
        }
        else
        {
            ClearSlot();
        }
    }

    public virtual void ClearSlot()
    {
        ItemData = null;
        ItemCount = 0;
        IsEquiped = false;
    }

    /// <summary>
    /// 인벤토리에서 아이템의 수량을 변경하는 함수
    /// </summary>
    /// <param name="count">획득한 갯수</param>
    public bool SetSlotCount(out uint oveCount, uint count = 1)
    {
        bool result;
        uint newCount = ItemCount + count;
        int over = (int)newCount - (int)ItemData.maxItemCount;

        if (over > 0)
        {
            ItemCount = ItemData.maxItemCount;
            oveCount = (uint)over;
            result = false;

        }
        else
        {
            ItemCount = newCount;
            oveCount = 0;
            result = true;
        }
        return result;
    }

    /// <summary>
    /// 이 슬롯의 아이템 개수를 감소시키는 함수
    /// </summary>
    /// <param Name="decreaseCount">감소시킬 개수</param>
    public void DecreaseSlotItem(uint count = 1)
    {
        int newCount = (int)ItemCount - (int)count;
        if (newCount > 0)
        {
            // 아직 아이템이 남아있음
            ItemCount = (uint)newCount;
        }
        else
        {
            ClearSlot();
        }
    }


    /// <summary>
    /// 이 슬롯에 있는 아이템을 장비하는 함수
    /// </summary>
    //public void EquipItem()
    //{
    //    Player player = GameManager.Instance.Player;
    //    if (player != null)
    //    {
    //        // 플레이어의 아이템 장비하는 함수
    //        player.Equipped(ItemData.itemType);
    //    }
    //}
}
