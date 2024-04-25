using System;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    private static readonly int Default_Inventory_Size = 144; // 고정 인벤토리 크기
    [SerializeField]
    private ItemSlot[] items; // 상점에서 판매할 아이템 슬롯 배열

    private ItemDataManager itemDataManager;
    private ShopInventoryUI shopInvenUI;

    void Awake()
    {
        items = new ItemSlot[Default_Inventory_Size]; // 배열 초기화
        InitializeInventory();
        itemDataManager = GameManager.Instance.ItemData;
        shopInvenUI = GameManager.Instance.ShopInventoryUI;
    }

    private void InitializeInventory()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new ItemSlot((uint)i); // 각 슬롯 초기화
        }
    }

    public void AddItem(ItemCode code)

    {
        if (itemDataManager == null)
        {
            Debug.LogError("ItemDataManager is not initialized.");
            return;
        }
        ItemData data = itemDataManager[code];
        if (data != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].IsEmpty)
                {
                    items[i].AssignSlotItem(data);
                    UpdateUI();
                    return;
                }
                else if (items[i].ItemData == data)
                {
                    // 아이템이 같은 경우 수량 증가
                    if (items[i].SetSlotCount(out uint overCount, 1))
                    {
                        UpdateUI();
                        return;
                    }
                }
            }
            Debug.LogError("No empty slots or space available to add new item.");
        }
        else
        {
            Debug.LogError($"Item with code {code} not found.");
        }
    }

    public ItemData GetItem(ItemCode code)
    {
        foreach (var slot in items)
        {
            if (!slot.IsEmpty && slot.ItemData.itemId == code)
            {
                return slot.ItemData;
            }
        }
        return null;
    }

    public void RemoveItem(ItemCode code)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (!items[i].IsEmpty && items[i].ItemData.itemId == code)
            {
                items[i].ClearSlot();
                UpdateUI();
                return;
            }
        }
    }

    private void UpdateUI()
    {
        if (shopInvenUI != null)
        {
            // 각 슬롯에 대해 UI 갱신 요청
            for (int i = 0; i < items.Length; i++)
            {
                if (shopInvenUI.shopSlots.Length > i && shopInvenUI.shopSlots[i] != null)
                {
                    // 각 Slot_UI 객체에 ItemSlot 정보를 기반으로 UI를 갱신하도록 요청
                    shopInvenUI.shopSlots[i].InitializeSlot(items[i]);
                }
            }
        }
        else
        {
            Debug.LogError("ShopInventoryUI component is not initialized.");
        }
    }

}
