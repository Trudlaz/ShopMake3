using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectMenuUI : MonoBehaviour
{
    PlayerInput inputActions;

    ItemSlot targetSlot;

    /// <summary>
    /// 아이템 구매 버튼을 누르면 호출되는 델리게이트
    /// </summary>
    public Action<ItemSlot> onItemBuy;

    /// <summary>
    /// 아이템 판매 버튼을 누르면 호출되는 델리게이트
    /// </summary>
    public Action<ItemSlot> onItemSell;

    private void Awake()
    {
        inputActions = new PlayerInput();

        // 구매 버튼
        Transform child = transform.GetChild(0);
        Button buyButton = child.GetComponent<Button>();
        buyButton.onClick.AddListener(() =>
        {
            onItemBuy?.Invoke(targetSlot);
        });

        // 판매 버튼
        child = transform.GetChild(1);
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
