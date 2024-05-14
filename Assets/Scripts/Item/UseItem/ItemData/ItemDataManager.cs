using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    public ItemData[] items = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ItemData this[ItemCode type] => items[(int)type];

    /// <summary>
    /// 아이템의 개수?
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public ItemData this[int index] => items[index];
    public ItemData GetItemDataByCode(ItemCode code)
    {
        // items 배열을 순회하며 일치하는 아이템 코드의 데이터를 찾음
        foreach (var item in items)
        {
            if (item.itemId == code)
            {
                return item;
            }
        }
        return null; // 일치하는 아이템이 없으면 null 반환
    }
}
