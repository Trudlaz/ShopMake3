using System;

[Serializable]
public class ItemSlotData
{
    /// <summary>
    /// 아이템 코드
    /// </summary>
    public ItemCode ItemCode;

    /// <summary>
    /// 아이템 수량
    /// </summary>
    public uint ItemCount;

    /// <summary>
    /// 장착 여부
    /// </summary>
    public bool IsEquipped;
}
