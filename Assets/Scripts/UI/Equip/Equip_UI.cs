using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equip_UI : MonoBehaviour
{
    // 필요한 변수들과 컴포넌트에 대한 참조
    [SerializeField] private EquipSlot_UI[] equipSlot_UI;
    [SerializeField] private DropSlotUI dropSlot;
    [SerializeField] private InventoryManager invenManager;
    [SerializeField] private RectTransform invenTransform;
    [SerializeField] private CanvasGroup canvas;
    private Equip equip;
    private PlayerInput inputActions;

    // 장비창 플레이어 소유자 참조
    private Player Owner => equip.Owner;

    private void Awake()
    {
        inputActions = new PlayerInput();

        // EquipSlot_UI 배열 초기화
        Transform child = transform.GetChild(0);
        equipSlot_UI = child.GetComponentsInChildren<EquipSlot_UI>();

        // DropSlotUI 초기화
        child = transform.GetChild(1);
        dropSlot = child.GetComponent<DropSlotUI>();

        // 인벤토리 매니저와 UI 요소 초기화
        invenManager = GetComponentInParent<InventoryManager>();
        invenTransform = GetComponent<RectTransform>();
        canvas = GetComponent<CanvasGroup>();
    }

    // 인벤토리 UI 초기화
    public void InitializeInventory(Equip playerEquip)
    {
        equip = playerEquip;

        // 각 장비 슬롯 UI 초기화 및 이벤트 연결
        for (uint i = 0; i < equipSlot_UI.Length; i++)
        {
            equipSlot_UI[i].InitializeSlot(equip[i]);
            equipSlot_UI[i].onDragBegin += OnItemMoveBegin;
            equipSlot_UI[i].onDragEnd += OnItemMoveEnd;
            equipSlot_UI[i].OnClick += OnClick;
        }
        invenManager.DragSlot.InitializeSlot(equip.DragSlot);
        dropSlot.Close();
    }

    /// <summary>
    /// 아이템 드래그 시작 시 호출
    /// </summary>
    private void OnItemMoveBegin(ItemSlot slot)
    {
        invenManager.DragSlot.InitializeSlot(equip.DragSlot);
        equip.MoveItem(slot, invenManager.DragSlot.ItemSlot);
        invenManager.DragSlot.Open();
    }

    /// <summary>
    /// 아이템 드래그 종료 시 호출
    /// </summary>
    private void OnItemMoveEnd(ItemSlot slot, RectTransform rect)
    {
        equip.MoveItem(invenManager.DragSlot.ItemSlot, slot);
        Inventory_UI inven = FindObjectOfType<Inventory_UI>();

        if (invenManager.DragSlot.ItemSlot.IsEmpty)
        {
            invenManager.DragSlot.Close();
        }
    }

    /// <summary>
    /// 아이템 슬롯 클릭 시 호출
    /// </summary>
    private void OnClick(ItemSlot slot, RectTransform rect)
    {
        if (!invenManager.DragSlot.ItemSlot.IsEmpty)
        {
            OnItemMoveEnd(slot, rect);
        }
    }

    // 인벤토리 열기
    public void Open()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    // 인벤토리 닫기
    public void Close()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    // 인벤토리 토글
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
