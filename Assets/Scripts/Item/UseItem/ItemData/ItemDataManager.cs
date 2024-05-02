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
}
