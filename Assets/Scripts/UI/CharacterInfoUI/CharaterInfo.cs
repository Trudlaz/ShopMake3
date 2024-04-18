using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterInfo : MonoBehaviour
{
    //[Tooltip("슬롯들 배열 길이")]
    private int slotLength = 0;
    [Tooltip("슬롯들 배열. 길이는 정비창 슬롯 갯수 : 8")]
    [SerializeField] private Slot[] slots = new Slot[8];

    private Slot_UI slotDrag;


    private void Awake()
    {

    }

    private void CollectSlotsData()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = GetComponentsInChildren<Slot>()[i];
            Debug.Log(slots[i]);
        }
    }

    /// <summary>
    /// 아이템 추가 함수 호출해서 추가
    /// </summary>
    /// <param name="item">추가할 아이템</param>
    private void AddItemData(ItemData item)
    {
        bool addSuccess = false;

        for (int i = 0; i < slots.Length; i++)
        {
            addSuccess = slots[i].AddItem(item);

            if (addSuccess)
            {
                break;
            }
        }
    }
}
