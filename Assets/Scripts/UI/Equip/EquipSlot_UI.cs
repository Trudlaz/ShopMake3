using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EquipSlot_UI : Slot_UI_Base
{
    [Tooltip("해당 슬롯의 타입 할당")]
    public List<ItemType> slotType = new List<ItemType>();

    EquipSlot equipSlot;



    public Action<ItemSlot> onDragBegin;
    public Action<ItemSlot, RectTransform> onDragEnd;


    public Action<ItemSlot, RectTransform> OnClick;

    public Action<uint> onRightClick;

    protected override void Awake()
    {
        Transform child = transform.GetChild(0);
        itemImage = child.GetComponent<Image>();
    }

    public override void InitializeSlot(ItemSlot slot)
    {
        equipSlot = slot as EquipSlot;

        equipSlot.onSlotItemChange = Refresh;   // 슬롯의 아이템에 변경이 있을 때 Refresh함수 실행(이전에 연결된 함수는 모두 무시)
    }



    protected override void OnRefresh()
    {
        base.OnRefresh();
    }

    private void Refresh()
    {
        if (equipSlot.IsEmpty)
        {
            // 비어 있을 때
            itemImage.color = Color.clear;   // 아이콘 투명하게
            itemImage.sprite = null;         // 스프라이트 비우기
        }
        else
        {
            // 아이템이 들어있으면
            itemImage.sprite = equipSlot.ItemData.itemImage;      // 스프라이트 이미지 설정
            itemImage.color = Color.white;                       // 이미지 보이게 만들기

            switch (equipSlot.ItemData.itemType)
            {
                case ItemType.Gun:
                    Equipment = Equipment.Gun;
                    break;
                case ItemType.Grenade:
                    Equipment = Equipment.Throw;
                    break;
                case ItemType.Vest:
                    Equipment = Equipment.Vest;
                    break;
                case ItemType.Helmet:
                    Equipment = Equipment.Helmet;
                    break;
                case ItemType.BackPack:
                    Equipment = Equipment.BackPack;
                    break;
                case ItemType.Buff:
                    Equipment = Equipment.ETC;
                    break;
                case ItemType.Trap:
                    Equipment = Equipment.ETC;
                    break;
            }
        }
    }
}
