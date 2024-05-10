using UnityEngine;
using UnityEngine.UI;

public class MainButtonUI : MonoBehaviour
{
    public Button GameStart;
    public Button Shop;
    public Button GameEnd;

    public Button InShopBackButton;
    public Button InShopGoInvenButton;

    public Inventory_UI inventory;
    public ShopInventoryUI shopInventory;
    public WorldInventory_UI worldInventory;

    public GameObject buttonPanel; // 모든 버튼을 포함하는 부모 객체

    void Start()
    {
        // 각 버튼에 클릭 이벤트 리스너 추가
        GameStart.onClick.AddListener(StartGame);
        Shop.onClick.AddListener(ToggleShop);
        GameEnd.onClick.AddListener(EndGame);
        InShopBackButton.onClick.AddListener(HandleBackButton);
        InShopGoInvenButton.onClick.AddListener(ToggleInventory);

        // 초기 UI 설정: 모든 UI 비활성화 및 버튼 패널 활성화
        DeactivateAllUIs();
        buttonPanel.SetActive(true);
    }

    void StartGame()
    {
        GameManager.Instance.StartGame("InGameScene");
        DeactivateAllUIs();
    }

    void ToggleShop()
    {
        DeactivateAllUIs();
        shopInventory.gameObject.SetActive(true);
        worldInventory.gameObject.SetActive(true);
    }

    void EndGame()
    {
        GameManager.Instance.EndGame("MainMenuScene");
        DeactivateAllUIs();
    }

    void HandleBackButton()
    {
        // 모든 UI 비활성화하고 버튼 패널 활성화
        DeactivateAllUIs();
        buttonPanel.SetActive(true);
    }

    void ToggleInventory()
    {
        DeactivateAllUIs();
        inventory.gameObject.SetActive(true);
        worldInventory.gameObject.SetActive(true);
    }

    public void ShowButtons()
    {
        buttonPanel.SetActive(true); // 버튼 패널을 다시 활성화하는 함수
    }

    void OnDestroy()
    {
        // 리스너 제거
        GameStart.onClick.RemoveListener(StartGame);
        Shop.onClick.RemoveListener(ToggleShop);
        GameEnd.onClick.RemoveListener(EndGame);
        InShopBackButton.onClick.RemoveListener(HandleBackButton);
        InShopGoInvenButton.onClick.RemoveListener(ToggleInventory);
    }

    private void DeactivateAllUIs()
    {
        inventory.gameObject.SetActive(false);
        shopInventory.gameObject.SetActive(false);
        worldInventory.gameObject.SetActive(false);
    }
}
