using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    PlayerInput inputActions;

    Inventory inventory;

    Slot_UI[] slotsUI;

    //DragSlotUI dragSlotUI;

    SelectMenuUI select;

    DropSlotUI dropSlot;

    WeightPanel_UI weightPanel;

    MoneyPanel_UI moneyPanel;

    InventoryManager invenManager;

    RectTransform invenTransform;

    Player Owner => inventory.Owner;

    CanvasGroup canvas;

    // 무게는 이후에 플레이어로 옮긴 이후 무게에 따라서 이동속도 변환 기믹 구현하기
    int weight = 0;
    public int Weight
    {
        get => weight;
        set
        {
            if (weight != value)
            {
                weight = Math.Max(0, value);
                onWeightChange?.Invoke(weight);
            }
        }
    }

    int money = 0;

    /// <summary>
    /// 인벤토리 돈 관리하는 프로퍼티인데 나중에 아이템 개수로 바꿀 예정
    /// </summary>
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


    /// <summary>
    /// 인벤토리의 무게에 변화가 있으면 실행되는 델리게이트
    /// </summary>
    public Action<int> onWeightChange;

    /// <summary>
    /// 인벤토리의 돈에 변화가 있으면 실행되는 델리게이트
    /// </summary>
    public Action<int> onMoneyChange;

    /// <summary>
    /// 아이템을 장비했다고 알리는 델리게이트(ItemSlot : 장비한 아이템의 슬롯에 대한 정보)
    /// </summary>
    public Action<ItemSlot> onEquipped; 


    private void Awake()
    {
        inputActions = new PlayerInput();

        Transform child = transform.GetChild(0);
        slotsUI = child.GetComponentsInChildren<Slot_UI>();

        child = transform.GetChild(1);
        select = child.GetComponent<SelectMenuUI>();

        child = transform.GetChild(2);
        dropSlot = child.GetComponent<DropSlotUI>();

        child = transform.GetChild(3);
        weightPanel = child.GetComponent<WeightPanel_UI>();

        child = transform.GetChild(4);
        moneyPanel = child.GetComponent<MoneyPanel_UI>();

        invenManager = GetComponentInParent<InventoryManager>();

        invenTransform = GetComponent<RectTransform>();

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

    public void InitializeInventory(Inventory playerInventory)
    {
        inventory = playerInventory;

        for (uint i = 0; i < slotsUI.Length; i++)
        {
            slotsUI[i].InitializeSlot(inventory[i]);
            slotsUI[i].onDragBegin += OnItemMoveBegin;
            slotsUI[i].onDragEnd += OnItemMoveEnd;
            slotsUI[i].onRightClick += OnRightClick;
            slotsUI[i].OnClick += OnClick;
        }
        invenManager.DragSlot.InitializeSlot(inventory.DragSlot);  // 임시 슬롯 초기화

        select.onItemDrop += OnItemDrop;
        select.onItemSort += (by) =>
        {
            inventory.MergeItems();
            OnItemSort(by);
        };
        select.Close();

        dropSlot.onDropOk += OnDropOk;
        dropSlot.Close();

        onWeightChange += weightPanel.Refresh;
        weightPanel.Refresh(Weight);

        onMoneyChange += moneyPanel.Refresh;
        moneyPanel.Refresh(Money);

        Close();
    }

    public void PlusValue(ItemSlot slot)
    {
        inventory.PlusValue(slot, (int)slot.ItemCount);
    }

    /// <summary>
    /// 게임이 끝난 이후 로컬인벤토리 정리하고 메인화면으로 나가는 함수
    /// </summary>
    public void InventoryResult()
    {
        //int tenThousand = Money / 10000;
        //int Thousand = (Money % 10000) / 1000;
        //int hundred = (Money % 1000) / 100;

        //Debug.Log($"10000원 {tenThousand}장 1000원 {Thousand}장 100원 {hundred}개");

        GameManager game = GameManager.Instance;

        game.WorldInventory_UI.Money += Money;
        inventory.ClearInventory();
        Money = 0;
        Weight = 0;

        // 이후에 메인화면으로 나가기
    }



    /// <summary>
    /// 아이템 드래그 시작하면 실행되는 함수
    /// </summary>
    /// <param name="index">시작한 슬롯의 index</param>
    private void OnItemMoveBegin(ItemSlot slot)
    {
        invenManager.DragSlot.InitializeSlot(inventory.DragSlot);  // 임시 슬롯 초기화
        inventory.MoveItem(slot, invenManager.DragSlot.ItemSlot);
        invenManager.DragSlot.Open();
    }

    /// <summary>
    /// 아이템 드래그가 끝이나면 실행되는 함수
    /// </summary>
    /// <param name="index">끝난 슬롯의 index</param>
    private void OnItemMoveEnd(ItemSlot slot, RectTransform rect)
    {
        inventory.MoveItem(invenManager.DragSlot.ItemSlot, slot);

        WorldInventory_UI worldInven;
        worldInven = rect.GetComponentInParent<WorldInventory_UI>();

        if (worldInven != null)
        {
            inventory.MinusValue(slot, (int)slot.ItemCount);
            worldInven.PlusValue(slot);
        }

        if (invenManager.DragSlot.ItemSlot.IsEmpty)
        {
            invenManager.DragSlot.Close();
        }

        // 마우스를 땟을 때 위치가 장비창이라면 
        // slot.EquipItem();                    장비하고
        // 장비를 장비창에 복사하고(인벤토리에 있는 장비는 그대로 두고)
        // onEquipped?.Invoke(slot);            아이템 슬롯 정보 알려주기
    }

    /// <summary>
    /// 슬롯을 클릭하면 실행되는 함수
    /// </summary>
    /// <param name="index"></param>
    private void OnClick(ItemSlot slot, RectTransform rect)
    {
        if(!invenManager.DragSlot.ItemSlot.IsEmpty)
        {
            OnItemMoveEnd(slot, rect);
        }
    }

    /// <summary>
    /// 슬롯을 우클릭 시 실행되는 함수
    /// </summary>
    /// <param name="index">우클릭한 슬롯의 index</param>
    private void OnRightClick(uint index)
    {
        // 버리기, 상세보기 등 UI따로 띄우기
        Slot_UI target = slotsUI[index];
        select.Open(target.ItemSlot);
    }

    /// <summary>
    /// 인벤토리를 정렬하는 함수
    /// </summary>
    /// <param name="type">정렬되는 순서</param>
    private void OnItemSort(ItemType type)
    {
        inventory.SlotSorting(type, true);
        select.Close();
    }

    /// <summary>
    /// 아이템을 버리는 함수
    /// </summary>
    /// <param name="index"></param>
    private void OnItemDrop(uint index)
    {
        Slot_UI target = slotsUI[index];
        select.Close();
        dropSlot.Open(target.ItemSlot);
    }

    /// <summary>
    /// 버리기 창에서 확인 버튼을 누르면 실행되는 함수
    /// </summary>
    /// <param name="index">아이템을 버릴 슬롯의 index</param>
    /// <param name="count">아이템 버릴 개수</param>
    private void OnDropOk(uint index, uint count)
    {
        inventory.RemoveItem(index, count);
        dropSlot.Close();
    }

    public void open()
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
            open();
        }
    }
}
