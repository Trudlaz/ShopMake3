using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot_UI : Slot_UI_Base, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Action<ItemSlot> onDragBegin;
    public Action<ItemSlot, RectTransform> onDragEnd;

    public Action<ItemSlot, RectTransform> OnClick;

    public Action<uint> onRightClick;

    TextMeshProUGUI equippedText;

    private InventoryManager invenManager;

    protected override void Awake()
    {
        base.Awake();
        Transform child = transform.GetChild(2);
        equippedText = child.GetComponent<TextMeshProUGUI>();

        invenManager = GetComponentInParent<InventoryManager>();
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
            RectTransform rect = GetComponent<RectTransform>();
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
            //EquipSlot_UI equipSlot = obj.GetComponent<EquipSlot_UI>();
            if (endSlot != null)
            {
                RectTransform rect = obj.GetComponent<RectTransform>();
                onDragEnd?.Invoke(endSlot.ItemSlot, rect);
            }
            //else if (equipSlot != null)
            //{
            //    RectTransform rect = null;
            //    ItemSlot slot = null;
            //    foreach (var slotType in equipSlot.slotType)
            //    {
            //        if (slotType == invenManager.DragSlot.ItemSlot.ItemData.itemType)
            //        {
            //            rect = obj.GetComponent<RectTransform>();
            //            slot = equipSlot.ItemSlot;
            //            ItemSlot.IsEquiped = true;
            //            break;
            //        }
            //    }
            //    rect = GetComponent<RectTransform>();
            //    onDragEnd?.Invoke(slot, rect);
            //}
            else
            {
                RectTransform rect = GetComponent<RectTransform>();
                onDragEnd?.Invoke(ItemSlot, rect);
            }
        }
        else
        {
            RectTransform rect = GetComponent<RectTransform>();
            onDragEnd?.Invoke(ItemSlot, rect);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
