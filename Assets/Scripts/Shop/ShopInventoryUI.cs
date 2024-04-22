using System;
using UnityEngine;

public class ShopInventoryUI : MonoBehaviour
{
    // 상점에서 사용할 컴포넌트 및 변수 선언
    Slot_UI[] shopSlots;
    RectTransform shopTransform;
    CanvasGroup canvas;
    ShopBuyMenuUI shopBuyMenu;  // ShopBuyMenuUI 컴포넌트를 위한 참조

    private void Awake()
    {
        // 컴포넌트 초기화: 각 UI 컴포넌트를 찾아서 변수에 할당
        Transform child = transform.GetChild(0);
        shopSlots = child.GetComponentsInChildren<Slot_UI>();
        shopTransform = GetComponent<RectTransform>();
        canvas = GetComponent<CanvasGroup>();
        shopBuyMenu = GetComponentInChildren<ShopBuyMenuUI>();  // ShopBuyMenuUI 컴포넌트 찾기
    }

    public void InitializeShop()
    {
        // 슬롯 UI 초기화
        for (uint i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].InitializeSlot(shopSlots[i].ItemSlot);  // 예시 아이템 데이터
            shopSlots[i].onRightClick += OnItemSell;  // 판매 함수 연결
        }
    }

    // 아이템 판매 함수
    private void OnItemSell(uint index)
    {
        Slot_UI target = shopSlots[index];
        if (target.ItemSlot.ItemData.itemType != ItemType.Price)  // 판매 가능한 아이템인지 확인
        {
            Debug.Log($" {target.ItemSlot} 아이템을 팔았습니다.");
            shopBuyMenu.OpenBuyMenu(target.ItemSlot);  // ShopBuyMenuUI의 구매 메뉴 열기 메서드 호출
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
