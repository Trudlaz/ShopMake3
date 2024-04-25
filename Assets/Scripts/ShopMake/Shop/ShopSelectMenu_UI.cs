using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopSelectMenu_UI : MonoBehaviour
{
    ItemSlot targetSlot;            // 현재 선택된 아이템 슬롯
    public Action<ItemSlot> onItemPurchase; // 구매 버튼 델리게이트
    PlayerInput inputActions;       // 플레이어 입력 액션
    public ShopBuyMenuUI shopBuyMenuUI; // 구매 메뉴 UI

    private void Awake()
    {
        // 플레이어 입력 액션 초기화
        inputActions = new PlayerInput();

        // 자식 객체 중 첫 번째 자식의 버튼 컴포넌트를 가져와서 클릭 이벤트 설정
        Transform child = transform.GetChild(0);
        Button purchaseButton = child.GetComponent<Button>();
        purchaseButton.onClick.AddListener(() =>
        {
            if (!targetSlot.IsEmpty)
            {
                // ShopBuyMenuUI 활성화 및 설정
                //shopBuyMenuUI.gameObject.SetActive(true);
               // shopBuyMenuUI.Setup(targetSlot);
                // 구매 이벤트 호출
                onItemPurchase?.Invoke(targetSlot);
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

    // 아이템 선택 메뉴 열기
    public void Open(ItemSlot target)
    {
        if (!target.IsEmpty)
        {
            targetSlot = target;
            gameObject.SetActive(true);
            MovePosition(Mouse.current.position.ReadValue());
        }
    }

    // 아이템 선택 메뉴 닫기
    public void Close()
    {
        gameObject.SetActive(false);
    }

    // 마우스 클릭 시 실행되는 이벤트
    private void OnClick(InputAction.CallbackContext context)
    {
        // 메뉴 밖을 클릭하면 메뉴 닫기
        if (!MousePointInRect())
        {
            Close();
        }
    }

    // 마우스 위치가 메뉴 안에 있는지 확인하는 함수
    bool MousePointInRect()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector2 diff = screenPos - (Vector2)transform.position;
        RectTransform rectTransform = (RectTransform)transform;
        return rectTransform.rect.Contains(diff);
    }

    // 메뉴 위치 조정하는 함수
    public void MovePosition(Vector2 screenPos)
    {
        RectTransform rect = (RectTransform)transform;
        int over = (int)(screenPos.x + rect.sizeDelta.x) - Screen.width;
        screenPos.x -= Mathf.Max(0, over);
        rect.position = screenPos;
    }
}
