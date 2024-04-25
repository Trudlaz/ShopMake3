using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Purchasing;
using UnityEngine;

public class QuickSlot : MonoBehaviour
{
    //private Player player;
    //private GameObject quickSlot;
    //private Action quickSlotSelect;
    public Equip equip;
    GameObject item;
    [Tooltip("생성할 아이템 오브젝트의 부모 오브젝트")]
    [SerializeField] private GameObject itemPostion;

    private void Awake()
    {
        //equip = FindObjectOfType<Equip>(true);
        //quickSlot = Fin
    }

    private void Start()
    {
        //player = FindObjectOfType<Player>();
    }

    /// <summary>
    /// 선택한 아이템 오브젝트 생성
    /// </summary>
    /// <param name="index"></param>
    public void ItemObjectCreate(uint index)
    {
        if (item == null)
        {
            item = equip.slots[index].ItemData.itemPrefab;
            Instantiate(item, itemPostion.transform.position, Quaternion.identity, transform);
        }
        else
        {
            Destroy(item);
            item = equip.slots[index].ItemData.itemPrefab;
        }
    }

    public void ItemInputEventEnroll()
    {
        // 아이템의 use 함수 이벤트 등록
    }
}
