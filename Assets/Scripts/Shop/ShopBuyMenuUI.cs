using UnityEngine;
using UnityEngine.UI;

public class ShopBuyMenuUI : MonoBehaviour
{
    public Text itemNameText;         // 아이템 이름을 표시할 텍스트 필드
    public Image itemIcon;            // 아이템 아이콘을 표시할 이미지
    public Text itemPriceText;        // 아이템 가격을 표시할 텍스트 필드
    public InputField quantityInput;  // 구매할 수량을 입력받을 인풋 필드
    public Button purchaseButton;     // 구매를 실행할 버튼
    public Button cancelButton;       // 구매 취소 혹은 창 닫기 버튼

    private ItemSlot currentItemSlot; // 현재 선택된 아이템 슬롯

    public delegate void BuyAction(ItemSlot slot, uint quantity);
    public event BuyAction OnBuyItem;

    private void Start()
    {
        purchaseButton.onClick.AddListener(() => AttemptPurchase());
        cancelButton.onClick.AddListener(CloseMenu);
    }

    public void Setup(ItemSlot itemSlot)
    {
        currentItemSlot = itemSlot;
        itemNameText.text = itemSlot.ItemData.itemName;
        itemIcon.sprite = itemSlot.ItemData.itemImage;
        itemPriceText.text = $"Price: {itemSlot.ItemData.Price} Coins";
        quantityInput.text = "1";  // Default quantity
    }

    private void AttemptPurchase()
    {
        if (uint.TryParse(quantityInput.text, out uint quantity))
        {
            OnBuyItem?.Invoke(currentItemSlot, quantity);
        }
        else
        {
            Debug.LogError("Invalid quantity input.");
        }
    }

    private void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }
}
