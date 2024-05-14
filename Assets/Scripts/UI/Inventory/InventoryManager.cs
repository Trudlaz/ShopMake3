using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 인벤토리 및 장비창 열고 닫기용 클래스
public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// 플레이어가 탈출하고 아이템 정산이 끝나면 누르는 버튼
    /// </summary>
    Button resultButton;

    DragSlotUI dragSlot;
    Inventory_UI inven;
    WorldInventory_UI worldInven;
    Equip_UI equip;

    public DragSlotUI DragSlot => dragSlot;

    PlayerInput input;

    bool inventoryOnOff = true;


    private void Awake()
    {
        input = new PlayerInput();

        dragSlot = GetComponentInChildren<DragSlotUI>();

        inven = GetComponentInChildren<Inventory_UI>();
        worldInven = GetComponentInChildren<WorldInventory_UI>();
        equip = GetComponentInChildren<Equip_UI>();

        Transform child = transform.GetChild(3);
        resultButton = child.GetComponent<Button>();

    }

    private void OnEnable()
    {
        input.UI.Enable();
        input.UI.Inventory.performed += OnOpen;
    }

    private void OnDisable()
    {
        input.UI.Inventory.performed -= OnOpen;
        input.UI.Disable();
    }



    private void Start()
    {
        GameManager.Instance.OnGameEnding += () =>
        {
            resultButton.gameObject.SetActive(true);
        };

        //resultButton.onClick.AddListener(OnClick);
        //resultButton.gameObject.SetActive(false);
    }

    private void OnClick()
    {
        if (inven != null)
        {
            inven.InventoryResult();
        }
        resultButton.gameObject.SetActive(false);
    }


    private void OnOpen(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inven.InventoryOnOff();
        equip.InventoryOnOff();
    }
}
