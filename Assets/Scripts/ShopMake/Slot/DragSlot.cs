using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSlot : ItemSlot
{
    const uint NotSet = uint.MaxValue;
    uint fromIndex = NotSet;

    public uint FromIndex => fromIndex;

    public DragSlot(uint index) : base(index)
    {
        fromIndex = NotSet;
    }

    public override void ClearSlot()
    {
        base.ClearSlot();
        fromIndex = NotSet;
    }

    public void SetFromIndex(uint index)
    {
        fromIndex = index;
    }
}
