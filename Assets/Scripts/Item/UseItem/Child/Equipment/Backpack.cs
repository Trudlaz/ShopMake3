using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Backpack : ItemBase
{

    //public float maxWeight = 100.0f; // 최대 무게
    //private float currentWeight = 0.0f; // 현재 무게

    //// 아이템을 가방에 추가하는 함수?
    //public bool AddItem(ItemBase item)
    //{
    //    if (currentWeight + item.weight <= maxWeight)
    //    {
    //        items.Add(item);
    //        currentWeight += item.weight;
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    // 아이템을 가방에서 제거하는 함수?
    //public void RemoveItem(ItemBase item)
    //{
    //    if (items.Remove(item))
    //    {
    //        currentWeight -= item.weight;
    //        Debug.Log(item.name + "이(가) 가방에서 제거되었습니다.");
    //    }
    //}

    public override void Use()
    {

    }

    //public override void UnUse()
    //{

    //}
}
