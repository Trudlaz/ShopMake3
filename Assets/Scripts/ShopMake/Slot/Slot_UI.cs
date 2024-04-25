using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot_UI : Slot_UI_Base, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Action<ItemSlot> onDragBegin;
    public Action<ItemSlot, RectTransform> onDragEnd;

    public Action<ItemSlot, RectTransform> OnClick;

    public Action<uint> onRightClick;

    TextMeshProUGUI equippedText;

    protected override void Awake()
    {
        base.Awake();
        Transform child = transform.GetChild(2);
        equippedText = child.GetComponent<TextMeshProUGUI>();
    }

    protected override void OnRefresh()
    {
        if (ItemSlot.IsEquiped)
        {
            equippedText.color = Color.red;
        }
        else
        {
            equippedText.color = Color.clear;
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
