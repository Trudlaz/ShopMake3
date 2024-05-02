using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot_UI : Slot_UI_Base, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Tooltip("해당 슬롯의 타입 할당")]
    public List<ItemType> slotType = new List<ItemType>();

    public Action<ItemSlot> onDragBegin;
    public Action<ItemSlot, RectTransform> onDragEnd;


    public Action<ItemSlot, RectTransform> OnClick;

    public Action<uint> onRightClick;

    protected override void Awake()
    {
        //Transform child = transform.GetChild(0);
        //itemImage = child.GetComponent<Image>();
        base.Awake();
    }

    /// <summary>
    /// 슬롯UI의 화면 갱신
    /// </summary>
    private void Refresh()
    {
        if (ItemSlot.IsEmpty)
        {
            // 비어 있을 때
            itemImage.color = Color.clear;   // 아이콘 투명하게
            itemImage.sprite = null;         // 스프라이트 비우기
            //itemText.text = string.Empty;  // 글자도 제거
        }
        else
        {
            // 아이템이 들어있으면
            //if (!IsEquipment)
            //{
            //    // 아이템이 장비아이템이 아니면
            //    itemImage.sprite = ItemSlot.ItemData.itemImage;      // 스프라이트 이미지 설정
            //    itemImage.color = Color.white;                       // 이미지 보이게 만들기
            //    //itemText.text = ItemSlot.ItemCount.ToString();    // 아이템 개수 쓰기
            //    //itemText.alpha = 1;

            //}
            //else
            //{
            //    itemImage.sprite = ItemSlot.ItemData.itemImage;      // 스프라이트 이미지 설정
            //    itemImage.color = Color.white;                       // 이미지 보이게 만들기
            //    //itemText.text = ItemSlot.ItemCount.ToString();    // 아이템 개수 쓰기
            //    //itemText.alpha = 0;
            //}

            itemImage.sprite = ItemSlot.ItemData.itemImage;      // 스프라이트 이미지 설정
            itemImage.color = Color.white;                       // 이미지 보이게 만들기
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClick?.Invoke(Index);
        }
        else
        {
            RectTransform rect = transform.parent.parent.parent.parent.GetComponent<RectTransform>();
            OnClick?.Invoke(ItemSlot, rect);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        onDragBegin?.Invoke(ItemSlot);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;   // UI 대상 레이캐스트
        if (obj != null)
        {
            Slot_UI endSlot = obj.GetComponent<Slot_UI>();
            if (endSlot != null)
            {
                RectTransform rect = obj.GetComponent<RectTransform>();
                onDragEnd?.Invoke(endSlot.ItemSlot, rect);
            }
            else
            {
                RectTransform rect = transform.parent.parent.parent.parent.GetComponent<RectTransform>();
                onDragEnd?.Invoke(ItemSlot, rect);
            }
        }
        else
        {
            RectTransform rect = transform.parent.parent.parent.parent.GetComponent<RectTransform>();
            onDragEnd?.Invoke(ItemSlot, rect);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
