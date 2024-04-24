using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseSlotUI : MonoBehaviour
{
    private uint purchaseCount = 1;
    public uint MinItemCount = 1;  // 구매 수량의 최소값

    [SerializeField] private ItemSlot targetSlot;   // 현재 선택된 아이템 슬롯
    [SerializeField] private TMP_InputField inputField; // 구매 수량을 입력받는 필드
    [SerializeField] private Slider slider;          // 구매 수량을 조정하는 슬라이더

    public Action<ItemSlot, uint> onPurchaseConfirmed;  // 구매 확인 이벤트

    private void Awake()
    {
        // 입력 필드에서 값이 변경될 때 처리
        inputField.onValueChanged.AddListener(HandleInputValueChanged);
        // 슬라이더에서 값이 변경될 때 처리
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void Start()
    {
        // 인터페이스 초기화
        inputField.text = MinItemCount.ToString();
        UpdateUI();
    }

    public uint MaxItemCount
    {
        get
        {
            if (targetSlot == null)
            {
                Debug.LogError("targetSlot is null");
                return MinItemCount;
            }
            return targetSlot.IsEmpty ? MinItemCount : targetSlot.ItemCount;
        }
    }

    public uint PurchaseCount
    {
        get => purchaseCount;
        set
        {
            if (targetSlot == null)
            {
                Debug.LogError("Attempted to set PurchaseCount but targetSlot is null");
                return;
            }
            if (purchaseCount != value)
            {
                purchaseCount = (uint)Mathf.Clamp(value, MinItemCount, MaxItemCount);
                UpdateUI();
            }
        }
    }

    private void HandleInputValueChanged(string text)
    {
        if (uint.TryParse(text, out uint value))
        {
            PurchaseCount = value;
        }
    }

    private void HandleSliderValueChanged(float value)
    {
        PurchaseCount = (uint)value;
    }

    private void UpdateUI()
    {
        inputField.text = purchaseCount.ToString();
        slider.value = purchaseCount;
        slider.minValue = MinItemCount;
        slider.maxValue = MaxItemCount;
    }

    public void Open(ItemSlot slot)
    {
        targetSlot = slot;
        if (targetSlot != null && !targetSlot.IsEmpty)
        {
            gameObject.SetActive(true);
            PurchaseCount = MinItemCount;
            UpdateUI();
        }
    }

    public void ConfirmPurchase()
    {
        onPurchaseConfirmed?.Invoke(targetSlot, purchaseCount);
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
