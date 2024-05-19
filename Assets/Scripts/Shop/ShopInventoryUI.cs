using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopInventoryUI : MonoBehaviour
{
    public Slot_UI[] shopSlots; // 상점 슬롯 배열
    public RectTransform shopTransform; // 상점의 RectTransform
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

        // shopInventory가 null이면 새로 생성
        if (shopInventory == null)
        {
            shopInventory = new ShopInventory();
        }

        InitializeShopInventory();
    }

    public void InitializeShopInventory()
    {
        if (shopInventory == null)
        {
            Debug.LogError("shopInventory가 할당되지 않았습니다!");
            return;
        }

        for (uint i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].InitializeSlot(shopInventory[i]);
            shopSlots[i].onRightClick += OnItemRightClick;
        }

        shopBuyMenu.Close();
    }

    private void OnItemRightClick(uint index)
    {
        Slot_UI target = shopSlots[index];
        shopBuyMenu.Open(target.ItemSlot);
        shopBuyMenu.MovePosition(Mouse.current.position.ReadValue());
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < shopSlots.Length && i < shopInventory.items.Length; i++)
        {
            if (shopInventory.items[i] != null)
            {
                shopSlots[i].InitializeSlot(shopInventory.items[i]);
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
    public void AddBasicItem()
    {
        shopInventory.AddItem(ItemCode.Pistol);
        shopInventory.AddItem(ItemCode.Rifle);
        shopInventory.AddItem(ItemCode.Shotgun);
        shopInventory.AddItem(ItemCode.Sniper);
        shopInventory.AddItem(ItemCode.Key);
    }
}
