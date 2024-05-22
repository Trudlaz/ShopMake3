using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    public ItemData[] items = null; // 아이템 데이터를 저장하는 배열

    /// <summary>
    /// 아이템 코드를 통해 아이템 데이터를 가져오는 인덱서
    /// </summary>
    /// <param name="type">아이템 코드</param>
    /// <returns>아이템 데이터</returns>
    public ItemData this[ItemCode type] => items[(int)type];

    /// <summary>
    /// 인덱스를 통해 아이템 데이터를 가져오는 인덱서
    /// </summary>
    /// <param name="index">아이템 배열의 인덱스</param>
    /// <returns>아이템 데이터</returns>
    public ItemData this[int index] => items[index];

    /// <summary>
    /// 아이템 코드를 사용하여 아이템 데이터를 검색하는 메서드
    /// </summary>
    /// <param name="code">아이템 코드</param>
    /// <returns>해당 아이템 코드의 아이템 데이터</returns>
    public ItemData GetItemDataByCode(ItemCode code)
    {
        // items 배열을 순회하며 일치하는 아이템 코드의 데이터를 찾음
        foreach (var item in items)
        {
            if (item.itemId == code)
            {
                return item; // 일치하는 아이템 데이터를 반환
            }
        }
        return null; // 일치하는 아이템이 없으면 null 반환
    }
}
