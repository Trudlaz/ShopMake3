using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopInventoryUI : MonoBehaviour
{
    public Slot_UI[] shopSlots; // 상점 슬롯 배열
    public RectTransform shopTransform; // 상점의RectTransform
    public CanvasGroup canvas; // 상점 UI의 CanvasGroup
    public ShopBuyMenuUI shopBuyMenu; // ShopBuyMenuUI 컴포넌트 참조

    public ShopInventory shopInventory; // ShopInventory 참조

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
            shopBuyMenu.Close();
        }
    }

    private void OnItemRightClick(uint index)
    {
        Slot_UI target = shopSlots[index];
        shopBuyMenu.Open(target.ItemSlot);
        shopBuyMenu.MovePosition(Mouse.current.position.ReadValue());
            
    }


    public void UpdateSlots(ItemSlot[] updatedSlots)
    {
        // 인벤토리 슬롯 배열을 업데이트하는 로직
        for (int i = 0; i < shopSlots.Length && i < updatedSlots.Length; i++)
        {
            if (updatedSlots[i] != null)
            {
                shopSlots[i].InitializeSlot(updatedSlots[i]);
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
