using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    public ItemData[] items = null;

    /// <summary>
    /// 아이템 코드를 이용하여 아이템 데이터를 안전하게 가져옵니다.
    /// </summary>
    /// <param name="code">아이템 코드</param>
    /// <param name="itemData">찾아낸 아이템 데이터를 출력합니다.</param>
    /// <returns>데이터가 있으면 true, 없으면 false</returns>
    public bool TryGetItemData(ItemCode code, out ItemData itemData)
    {
        int index = (int)code;
        if (index >= 0 && index < items.Length)
        {
            itemData = items[index];
            return true;
        }
        itemData = null;
        return false;
    }

    /// <summary>
    /// 아이템 코드를 이용하여 아이템 데이터를 직접 반환합니다.
    /// </summary>
    /// <param name="type">아이템 코드</param>
    /// <returns>해당 코드의 아이템 데이터</returns>
    public ItemData this[ItemCode type] => items[(int)type];

    /// <summary>
    /// 인덱스를 이용하여 아이템 데이터를 직접 반환합니다.
    /// </summary>
    /// <param name="index">인덱스</param>
    /// <returns>해당 인덱스의 아이템 데이터</returns>
    public ItemData this[int index] => items[index];
}
