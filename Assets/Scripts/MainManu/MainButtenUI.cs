using UnityEngine;
using UnityEngine.UI;

public class MainButtonUI : MonoBehaviour
{
    public Button GameStart;
    public Button Shop;
    public Button Inventory;
    public Button GameEnd;

    public Inventory_UI inventory;
    public ShopInventoryUI shopInventory;
    public WorldInventory_UI worldInventory;

    public GameObject buttonPanel; // 모든 버튼을 포함하는 부모 객체

    void Start()
    {
        // 각 버튼에 클릭 이벤트 리스너 추가
        GameStart.onClick.AddListener(() => ToggleUI(StartGame));
        Inventory.onClick.AddListener(() => ToggleUI(ToggleInventory));
        Shop.onClick.AddListener(() => ToggleUI(ToggleShop));
        GameEnd.onClick.AddListener(() => ToggleUI(EndGame));

        // 초기 UI 설정: 모든 UI 비활성화 및 버튼 패널 활성화
        inventory.gameObject.SetActive(false);
        shopInventory.gameObject.SetActive(false);
        worldInventory.gameObject.SetActive(false);
        buttonPanel.SetActive(true);
    }

    void StartGame()
    {
        GameManager.Instance.StartGame("InGameScene");
    }

    void ToggleInventory()
    {
        inventory.gameObject.SetActive(true);
        worldInventory.gameObject.SetActive(true);
        shopInventory.gameObject.SetActive(false);
    }

    void ToggleShop()
    {
        shopInventory.gameObject.SetActive(true);
        worldInventory.gameObject.SetActive(true);
        inventory.gameObject.SetActive(false);
    }

    void EndGame()
    {
        GameManager.Instance.EndGame("MainMenuScene");
    }

    void ToggleUI(System.Action action)
    {
        action.Invoke();
        buttonPanel.SetActive(false); // 버튼 패널 비활성화
    }

    public void ShowButtons()
    {
        buttonPanel.SetActive(true); // 버튼 패널을 다시 활성화하는 함수
    }

    void OnDestroy()
    {
        // 리스너 제거
        GameStart.onClick.RemoveListener(() => ToggleUI(StartGame));
        Inventory.onClick.RemoveListener(() => ToggleUI(ToggleInventory));
        Shop.onClick.RemoveListener(() => ToggleUI(ToggleShop));
        GameEnd.onClick.RemoveListener(() => ToggleUI(EndGame));
    }
}
