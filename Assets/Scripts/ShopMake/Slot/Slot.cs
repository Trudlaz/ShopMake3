using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [Tooltip("해당 슬롯의 타입 할당")]
    public List<ItemType> slotType = new List<ItemType>();
    [Tooltip("아이템 저장할 정보")]
    [HideInInspector] public ItemData itemData = null;

    public delegate void ItemChanged();
    /// <summary>
    /// 아이템 ui 변경
    /// </summary>
    public event ItemChanged OnItemChanged;

    private ItemSlot itemslot;

    public bool IsEmpty => itemData == null;

    public bool AddItem(ItemData item)
    {
        if (item != null && slotType.Contains(item.itemType))
        {
            itemData = item;
            OnItemChanged?.Invoke();
            return true;
        }
        return false;
    }

    public void RemoveItem()
    {
        itemData = null;
        OnItemChanged?.Invoke();
    }

    public void EquipItem(ItemData item)
    {
        if (item != null && slotType.Contains(item.itemType))
        {
            itemData = item;
            OnItemChanged?.Invoke();
        }
    }
}