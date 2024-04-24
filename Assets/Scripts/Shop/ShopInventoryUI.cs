using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopInventoryUI : MonoBehaviour
{
    public Slot_UI[] shopSlots; // 상점 슬롯 배열
    public RectTransform shopTransform; // 상점의 RectTransform
    public CanvasGroup canvas; // 상점 UI의 CanvasGroup
    public ShopBuyMenuUI shopBuyMenu; // ShopBuyMenuUI 컴포넌트 참조

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        shopSlots = child.GetComponentsInChildren<Slot_UI>();
        shopTransform = GetComponent<RectTransform>();
        canvas = GetComponent<CanvasGroup>();
        shopBuyMenu = GetComponentInChildren<ShopBuyMenuUI>(); // ShopBuyMenuUI 컴포넌트 찾기

        InitializeShop();
    }

    public void InitializeShop()
    {
        foreach (Slot_UI slot in shopSlots)
        {
            slot.onRightClick += OnItemRightClick; // 우클릭 이벤트 연결
        }
    }

    private void OnItemRightClick(uint index)
    {
        if (index < shopSlots.Length)
        {
            Slot_UI targetSlot = shopSlots[index];
            if (!targetSlot.ItemSlot.IsEmpty)
            {
                shopBuyMenu.Open(targetSlot.ItemSlot);
                shopBuyMenu.MovePosition(Mouse.current.position.ReadValue());
            }
        }
    }

    // 상점 열기 함수
    public void Open()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    // 상점 닫기 함수
    public void Close()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}
