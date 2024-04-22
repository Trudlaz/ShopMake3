using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyMenuUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;         // 아이템 이름을 표시할 텍스트 필드
    public Image itemIcon;                // 아이템 아이콘을 표시할 이미지
    public TextMeshProUGUI itemPriceText;        // 아이템 가격을 표시할 텍스트 필드
    public TMP_InputField quantityInput;      // 구매할 수량을 입력받을 인풋 필드
    public Button purchaseButton;         // 구매를 실행할 버튼
    public Button cancelButton;           // 구매 취소 혹은 창 닫기 버튼
    public Slider quantitySlider;         // 수량을 조절할 슬라이더 (추가됨)

    private ItemSlot currentItemSlot;     // 현재 선택된 아이템 슬롯

    public uint MinItemCount = 1;         // 최소 구매 수량
    public uint MaxItemCount = 100;       // 최대 구매 수량

    // 아이템 구매 이벤트를 위한 델리게이트와 이벤트
    public delegate void BuyAction(ItemSlot slot, uint quantity);
    public event BuyAction OnBuyItem;

    private void Start()
    {
        // 버튼에 클릭 이벤트 리스너 추가
        purchaseButton.onClick.AddListener(() => AttemptPurchase());
        cancelButton.onClick.AddListener(CloseMenu);
    }

    // UI 설정
    public void Setup(ItemSlot itemSlot)
    {
        // 현재 아이템 슬롯 설정
        currentItemSlot = itemSlot;
        // UI 요소에 아이템 정보 설정
        itemNameText.text = itemSlot.ItemData.itemName;
        itemIcon.sprite = itemSlot.ItemData.itemImage;
        itemPriceText.text = $"가격 : {itemSlot.ItemData.Price}";
        quantityInput.text = "1";  // Default quantity
    }

    // 구매 메뉴 열기
    public bool OpenBuyMenu(ItemSlot target)
    {
        bool result = false;
        if (!target.IsEmpty && target.ItemCount >= MinItemCount)
        {
            Setup(target);  // Setup 호출로 UI 요소 설정
            quantitySlider.minValue = MinItemCount;
            quantitySlider.maxValue = Mathf.Min(MaxItemCount, target.ItemCount);
            quantitySlider.value = MinItemCount;

            result = true;
            gameObject.SetActive(true);
        }
        return result;
    }

    // 구매 시도
    private void AttemptPurchase()
    {
        // 수량 입력값이 유효한지 확인 후 이벤트 호출
        if (uint.TryParse(quantityInput.text, out uint quantity) && quantity >= MinItemCount && quantity <= MaxItemCount)
        {
            OnBuyItem?.Invoke(currentItemSlot, quantity);
        }
        else
        {
            Debug.LogError("잘못된 수량 입니다.");
        }
    }

    // 메뉴 닫기
    private void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }
}
