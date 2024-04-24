using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    PlayerInput inputActions;

    Inventory shopInventory; // 상점 인벤토리

    Slot_UI[] slotsUI;

    SelectMenuUI selectMenuUI; // 선택 메뉴 UI (구매 또는 판매)

    MoneyPanel_UI moneyPanel;

    InventoryManager invenManager;

    RectTransform shopTransform;

    CanvasGroup canvas;

    int money = 0;
    public int Money
    {
        get => money;
        set
        {
            if (money != value)
            {
                money = Math.Max(0, value);
                onMoneyChange?.Invoke(money);
            }
        }
    }

    public Action<int> onMoneyChange;

    private void Awake()
    {
        inputActions = new PlayerInput();
        Transform child = transform.GetChild(0);
        slotsUI = child.GetComponentsInChildren<Slot_UI>();

        selectMenuUI = GetComponentInChildren<SelectMenuUI>();
        moneyPanel = GetComponentInChildren<MoneyPanel_UI>();

        invenManager = GetComponentInParent<InventoryManager>();
        shopTransform = GetComponent<RectTransform>();

        canvas = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Inventory.performed += InventoryOnOff;
    }

    void OnDisable()
    {
        inputActions.UI.Inventory.performed -= InventoryOnOff;
        inputActions.UI.Disable();
    }

    public void InitializeShop(Inventory shopInventory)
    {
        this.shopInventory = shopInventory;
        for (uint i = 0; i < slotsUI.Length; i++)
        {
            slotsUI[i].InitializeSlot(shopInventory[i]);
            slotsUI[i].OnClick += OnItemSelect;
        }
        selectMenuUI.Close();
        onMoneyChange += moneyPanel.Refresh;
        moneyPanel.Refresh(Money);
        Close();
    }

    private void OnItemSelect(ItemSlot slot, RectTransform rect)
    {
        selectMenuUI.Open(slot);
        selectMenuUI.onItemBuy += OnBuyItem;
        selectMenuUI.onItemSell += OnSellItem;
    }

    private void OnBuyItem(ItemSlot slot)
    {
        if (Money >= slot.ItemData.Price)
        {
            Money -= (int)(slot.ItemData.Price);
            // 구매 로직
        }
    }

    private void OnSellItem(ItemSlot slot)
    {
        Money += (int)(slot.ItemData.Price);
        // 판매 로직
    }

    public void Open()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public void Close()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    private void InventoryOnOff(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (canvas.interactable)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
