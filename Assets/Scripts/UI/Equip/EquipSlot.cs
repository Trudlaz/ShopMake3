using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : ItemSlot
{
    public List<ItemType> slotType = new List<ItemType>();



    public EquipSlot(uint index) : base(index)
    {

    }

    public override void ClearSlot()
    {
        base.ClearSlot();
    }

    /// <summary>
    /// 이 슬롯에 있는 아이템을 장비하는 함수
    /// </summary>
    //public void Equip()
    //{
    //    Player player = GameManager.Instance.Player;

    //    if (player != null)
    //    {
    //        player.Equipped(ItemData.itemPrefab);
    //    }
    //}
}
