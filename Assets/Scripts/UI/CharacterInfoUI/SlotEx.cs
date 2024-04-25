using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotEx : MonoBehaviour
{
    [Tooltip("해당 슬롯의 타입 할당")]
    public List<ItemType> slotType = new List<ItemType>();
    [Tooltip("아이템 저장할 정보")]
    [HideInInspector] public ItemData itemData = null;
    [Tooltip("itemData에 저장할 아이템 정보 임시 저장소")]
    private ItemData itemTemData = null;

    private ItemData ItemData
    {
        get
        {
            return itemData;
        }
        set
        {
            itemTemData = value;

            if (CompareItemType)
            {
                itemData = value;
            }
            else
            {
                itemData = null;
            }
        }
    }

    /// <summary>
    /// 해당 아이템이 슬롯에 적합한지 검사 후 결과에 따라 참 / 거짓 리턴
    /// </summary>
    private bool CompareItemType
    {
        get
        {
            bool typeCheck = false;

            foreach (ItemType type in slotType)
            {
                if (itemTemData.itemType == type || itemTemData == null)
                {
                    typeCheck = true;

                    return typeCheck;
                }
            }

            return typeCheck;
        }
    }

    /// <summary>
    /// 슬롯에 아이템 저장. 인벤토리에 아이템 추가.
    /// </summary>
    /// <returns>아이템 추가가 성공이면 true 반환, 실패면 false 반환</returns>
    public bool AddItem(ItemData item)
    {
        bool addItem = false;

        if (item != null)
        {
            ItemData = item;
            addItem = true;
        }
        else
        {
            ItemData = null;
            addItem = false;
        }

        return addItem;
    }
}
