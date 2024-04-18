using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopSelectMenu_UI : MonoBehaviour
{
    ItemSlot targetSlot;  // 현재 선택된 아이템 슬롯
    public Action<ItemSlot> onItemPurchase; // 구매 버튼 델리게이트
    PlayerInput inputActions;
    public ShopBuyMenuUI shopBuyMenuUI; // 구매 메뉴 UI

    private void Awake()
    {
        inputActions = new PlayerInput();

        Transform child = transform.GetChild(0);
        Button purchaseButton = child.GetComponent<Button>();
        purchaseButton.onClick.AddListener(() =>
        {
            if (!targetSlot.IsEmpty)
            {
                shopBuyMenuUI.gameObject.SetActive(true); // ShopBuyMenuUI 활성화
                shopBuyMenuUI.Setup(targetSlot); // 활성화 시 필요한 슬롯 데이터 설정
                onItemPurchase?.Invoke(targetSlot); // 구매 이벤트 호출
            }
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
        gameObject.SetActive(false);
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
        Vector2 diff = screenPos - (Vector2)transform.position;
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
