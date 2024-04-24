using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WorldInventory_UI : MonoBehaviour
{
    // 필요한 컴포넌트 및 변수 선언
    WorldInventory worldInven;
    Slot_UI[] worldSlotUI;
    InventoryManager invenManager;
    WorldSelectMenuUI worldSelect;
    PurchaseSlotUI worldDropSlot;
    SellItemUI worldSellItem;
    Scrollbar scrollbar;
    MoneyPanel_UI moneyPanel;
    RectTransform worldTransform;
    CanvasGroup canvas;

    // 인벤토리에서 관리되는 돈
    int money = 0;

    /// <summary>
    /// 돈의 변화를 감지하고 UI를 업데이트 하는 프로퍼티
    /// </summary>
    public int Money
    {
        get => money;
        set
        {
            if (money != value)
            {
                money = value;
                onMoneyChange?.Invoke(money); // 돈의 변화가 있을 때 이벤트 발생
            }
        }
    }

    public Action<int> onMoneyChange; // 돈 변화 이벤트

    private void Awake()
    {
        // 컴포넌트 초기화
        Transform child = transform.GetChild(0);
        worldSlotUI = child.GetComponentsInChildren<Slot_UI>();
        worldSelect = GetComponentInChildren<WorldSelectMenuUI>();
        worldDropSlot = GetComponentInChildren<PurchaseSlotUI>();
        worldSellItem = GetComponentInChildren<SellItemUI>();
        moneyPanel = GetComponentInChildren<MoneyPanel_UI>();
        scrollbar = GetComponentInChildren<Scrollbar>();
        invenManager = GetComponentInParent<InventoryManager>();
        worldTransform = GetComponent<RectTransform>();
        canvas = GetComponent<CanvasGroup>();
    }

    public void InitializeWorldInventory(WorldInventory playerInventory)
    {
        if (playerInventory == null)
        {
            Debug.LogError("playerInventory is null");
            return;
        }
        // 다른 필드나 객체도 검사
        if (worldSlotUI == null)
        {
            Debug.LogError("worldSlotUI is null");
            return;
        }
        worldInven = playerInventory;

        // 슬롯 UI 초기화 및 이벤트 연결
        for (uint i = 0; i < worldSlotUI.Length; i++)
        {
            worldSlotUI[i].InitializeSlot(worldInven[i]);
            worldSlotUI[i].onDragBegin += OnItemMoveBegin;
          
            worldSlotUI[i].onRightClick += OnRightClick;
            worldSlotUI[i].OnClick += OnClick;
        }
        invenManager.DragSlot.InitializeSlot(worldInven.DragSlot);
        Debug.Log("Initializing World Inventory...");

        if (playerInventory == null)
        {
            Debug.LogError("playerInventory is null!");
            return;
        }

        Debug.Log("Player inventory is not null.");

        // 다른 필요한 객체들도 검사
        if (worldSlotUI == null)
        {
            Debug.LogError("worldSlotUI is null!");
            return;
        }

        Debug.Log("worldSlotUI is initialized.");

        // 문제가 발생하는 줄 전에 객체 상태 로깅
        Debug.Log($"Checking objects at line 88: {worldSlotUI.Length}, {playerInventory}");

        // 실제 초기화 코드
        for (uint i = 0; i < worldSlotUI.Length; i++)
        {
            if (worldSlotUI[i] == null)
            {
                Debug.LogError($"worldSlotUI at index {i} is null.");
                continue;
            }
            worldSlotUI[i].InitializeSlot(playerInventory[i]);
        }
        // 다양한 UI 이벤트 연결
        worldSelect.onItemDrop += OnItemDrop;
        worldDropSlot.onPurchaseConfirmed += OnPurchaseConfirmed;

        worldDropSlot.Close();
        worldSelect.onItemSort += (by) =>
        {
            worldInven.MergeItems();
            OnItemSort(by);
        };
        worldSelect.onItemSell += OnItemSell;
        worldSellItem.Close();
        worldSellItem.onSellOK += OnSellOk;
        worldSelect.Close();

        // 돈 변경 이벤트 연결
        onMoneyChange += moneyPanel.Refresh;
        moneyPanel.Refresh(Money);
        scrollbar.value = 1;
    }

    public void PlusValue(ItemSlot slot)
    {
        worldInven.PlusMoney(slot, (int)slot.ItemCount);
    }

    public void cleanInventory(Shop_UI inven)
    {
        Money += inven.Money;
    }

    // 아이템 드래그 시작시 호출
    private void OnItemMoveBegin(ItemSlot slot)
    {
        invenManager.DragSlot.InitializeSlot(worldInven.DragSlot);
        worldInven.MoveItem(slot, invenManager.DragSlot.ItemSlot);
        invenManager.DragSlot.Open();
    }

   

    // 슬롯 클릭시 호출
    private void OnClick(ItemSlot slot, RectTransform rect)
    {
        if (!invenManager.DragSlot.ItemSlot.IsEmpty)
        {
        }
    }
    private void OnPurchaseConfirmed(ItemSlot slot, uint count)
    {
        // 구매 로직을 여기에 구현합니다.
        // 예: 돈 차감, 아이템 수량 갱신 등
        int cost = (int)(slot.ItemData.Price * count);
        if (Money >= cost)
        {
            Money -= cost;
            // 아이템 추가
            if (worldInven.AddItem(slot.ItemData.itemId))
            {
                Debug.Log("아이템을 성공적으로 추가했습니다.");
                worldDropSlot.Close();
            }
            else
            {
                Debug.Log("아이템 추가에 실패했습니다. 인벤토리가 가득 찼을 수 있습니다.");
            }
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
            // 충분한 돈이 없을 경우 UI에 메시지 표시 등의 추가 처리
        }
    }


    // 슬롯 우클릭시 호출
    private void OnRightClick(uint index)
    {
        Slot_UI target = worldSlotUI[index];
        worldSelect.Open(target.ItemSlot);
    }

    // 인벤토리 정렬 함수
    private void OnItemSort(ItemType type)
    {
        worldInven.SlotSorting(type, true);
        worldSelect.Close();
    }

    // 아이템 버리기 함수
    private void OnItemDrop(uint index)
    {
        Slot_UI target = worldSlotUI[index];
        worldSelect.Close();
        worldDropSlot.Open(target.ItemSlot);
    }

    // 아이템 판매 함수
    private void OnItemSell(ItemSlot slot)
    {
        worldSelect.Close();
        if (slot.ItemData.itemType != ItemType.Price) // 판매 가능한 아이템인지 체크
        {
            worldSellItem.Open(slot);
        }
    }

    // 아이템 버리기 확인 버튼 눌렀을 때 호출
    private void OnDropOk(uint index, uint count)
    {
        worldInven.RemoveItem(index, count);
        worldInven.MinusMoney(worldSlotUI[index].ItemSlot, (int)count);
        worldDropSlot.Close();
    }

    // 아이템 판매 확인 버튼 눌렀을 때 호출
    private void OnSellOk(ItemSlot slot, uint count)
    {
        worldInven.PlusMoney(slot, (int)count);
        worldInven.RemoveItem(slot.Index, count);
        worldSellItem.Close();
    }

    // 인벤토리 열기 함수
    public void open()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;

        invenManager.DragSlot.Close();
    }

    // 인벤토리 닫기 함수
    public void Close()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}
