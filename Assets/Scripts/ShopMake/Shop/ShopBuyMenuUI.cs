using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ShopBuyMenuUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText; // 아이템 이름 표시
    public Image itemIcon; // 아이템 아이콘 표시
    public TextMeshProUGUI itemPriceText; // 아이템 가격 표시
    public TMP_InputField quantityInput; // 수량 입력
    public Button purchaseButton; // 구매 버튼
    public Button cancelButton; // 취소 버튼
    public Slider quantitySlider; // 수량 슬라이더

    private ItemSlot currentItemSlot; // 현재 선택된 아이템 슬롯
    private PlayerInput inputActions; // 입력 시스템

    public uint MinItemCount = 1; // 최소 구매 수량
    public uint MaxItemCount = 100; // 최대 구매 수량

    public event Action<ItemSlot, uint> OnBuyItem; // 구매 이벤트

    private void Awake()
    {
        inputActions = new PlayerInput(); // 입력 시스템 초기화

        purchaseButton.onClick.AddListener(() => AttemptPurchase());
        cancelButton.onClick.AddListener(Close);
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        // 입력 시스템 활성화
        inputActions.UI.Enable();
        inputActions.UI.Click.performed += OnClick;
    }

    private void OnDisable()
    {
        // 입력 시스템 비활성화
        inputActions.UI.Click.performed -= OnClick;
        inputActions.UI.Disable();
    }


    public void Open(ItemSlot target)
    {
        if (!target.IsEmpty)
        {
            currentItemSlot = target;
            gameObject.SetActive(true);

            // UI 세팅
            itemNameText.text = target.ItemData.itemName;
            itemIcon.sprite = target.ItemData.itemImage;
            itemPriceText.text = $"가격 : {target.ItemData.Price}";
            quantityInput.text = "1";
            quantitySlider.minValue = MinItemCount;
            quantitySlider.maxValue = Mathf.Min(MaxItemCount, target.ItemCount);
            quantitySlider.value = MinItemCount;

            MovePosition(Mouse.current.position.ReadValue());
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void AttemptPurchase()
    {
        if (uint.TryParse(quantityInput.text, out uint quantity) && quantity >= MinItemCount && quantity <= MaxItemCount)
        {
            OnBuyItem?.Invoke(currentItemSlot, quantity);
        }
        else
        {
            Debug.LogError("Invalid quantity.");
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (Mouse.current != null && !MousePointInRect())
        {
            Close();
        }
    }

    private bool MousePointInRect()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector2 diff = screenPos - (Vector2)transform.position;

        RectTransform rectTransform = (RectTransform)transform;
        return rectTransform.rect.Contains(diff);
    }

    public void MovePosition(Vector2 screenPos)
    {
        RectTransform rect = (RectTransform)transform;
        Vector2 adjustedPos = screenPos;

        // 가로 방향 조정
        int horizontalOver = (int)(adjustedPos.x + rect.sizeDelta.x) - Screen.width;
        adjustedPos.x -= Mathf.Max(0, horizontalOver);

        // 세로 방향 조정
        int verticalOver = (int)(adjustedPos.y + rect.sizeDelta.y) - Screen.height;
        adjustedPos.y -= Mathf.Max(0, verticalOver);

        rect.position = adjustedPos;
    }

}
