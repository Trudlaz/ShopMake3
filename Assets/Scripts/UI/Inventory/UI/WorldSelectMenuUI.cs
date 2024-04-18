using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WorldSelectMenuUI : MonoBehaviour
{
    PlayerInput inputActions;

    ItemSlot targetSlot;

    /// <summary>
    /// 정렬버튼을 누르면 호출되는 델리게이트
    /// </summary>
    public Action<ItemType> onItemSort;

    /// <summary>
    /// 버리기 버튼을 누르면 호출되는 델리게이트
    /// </summary>
    public Action<uint> onItemDrop;

    /// <summary>
    /// 판매하기 버튼을 누르면 호출되는 델리게이트
    /// </summary>
    public Action<ItemSlot> onItemSell;



    private void Awake()
    {
        inputActions = new PlayerInput();

        Transform child = transform.GetChild(0);
        Button dropButton = child.GetComponent<Button>();
        dropButton.onClick.AddListener(() =>
        {
            // 아이템 버리기 UI띄우는 함수 만들어서 넣기 
            onItemDrop?.Invoke(targetSlot.Index);
        });

        child = transform.GetChild(1);
        Button sortButton = child.GetComponent<Button>();
        sortButton.onClick.AddListener(() =>
        {
            onItemSort?.Invoke(ItemType.Buff);
        });

        child = transform.GetChild(2);
        Button sellButton = child.GetComponent<Button>();
        sellButton.onClick.AddListener(() =>
        {
            onItemSell?.Invoke(targetSlot);
        });
    }


    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Click.performed += OnClick;
    }

    private void OnDisable()
    {
        inputActions.UI.Click.performed -= OnClick;
        inputActions.UI.Disable();
    }

    public void Open(ItemSlot target)
    {
        if (!target.IsEmpty)
        {
            targetSlot = target;
            gameObject.SetActive(true);

            MovePosition(Mouse.current.position.ReadValue());
        }
    }

    public void Close()
    {
        gameObject?.SetActive(false);
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (!MousePointInRect())
        {
            Close();
        }
    }

    bool MousePointInRect()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector2 diff = screenPos - (Vector2)transform.position; // 이 UI의 피봇에서 마우스 포인터가 얼마나 떨어져 있는지 계산

        RectTransform rectTransform = (RectTransform)transform;
        return rectTransform.rect.Contains(diff);
    }

    public void MovePosition(Vector2 screenPos)
    {
        RectTransform rect = (RectTransform)transform;

        int over = (int)(screenPos.x + rect.sizeDelta.x) - Screen.width;

        screenPos.x -= Mathf.Max(0, over);
        rect.position = screenPos;
    }
}
