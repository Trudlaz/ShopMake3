using UnityEngine;
using UnityEngine.UI;

public class MainButtonUI : MonoBehaviour
{
    public Button equipmentButton;
    public Button shopButton;
    public Button inventoryButton;
    public Button gameEndButton;

    public Button goInvenButton;
    public Button readyButton;
    public Button mainManuButton;
    public Button gamestartButton;

    public Equip_UI equop;
    public Inventory_UI inventory;
    public ShopInventoryUI shopInventory;
    public WorldInventory_UI worldInventory;

    public GameObject mainButtonPanel;
    public GameObject invenButtonPanel;

    void Start()
    {
        // 각 버튼에 클릭 이벤트 리스너 추가
        equipmentButton.onClick.AddListener(() => ToggleUI(Equipment));
        inventoryButton.onClick.AddListener(() => ToggleUI(ToggleInventory));
        shopButton.onClick.AddListener(() => ToggleUI(ToggleShop));
        gameEndButton.onClick.AddListener(() => EndGame());
        goInvenButton.onClick.AddListener(() => ToggleUI(ToggleInventory));
        readyButton.onClick.AddListener(() => ToggleUI(Equipment));
        mainManuButton.onClick.AddListener(() => ShowMainButtons());
        gamestartButton.onClick.AddListener(() => Gamestart());

        // 초기 UI 설정: 모든 UI 비활성화 및 버튼 패널 활성화
        SetAllUIElementsActive(false);
        mainButtonPanel.SetActive(true);
        invenButtonPanel.SetActive(false);
    }

    // 게임을 시작하는 메서드
    void Gamestart()
    {
        GameManager.Instance.StartGame("InGameScene");
    }

    // 장비 UI를 활성화하는 메서드
    void Equipment()
    {
        SetUIElementsState(true, true, false, false);
    }

    // 인벤토리 UI를 활성화하는 메서드
    void ToggleInventory()
    {
        SetUIElementsState(false, true, false, true);
    }

    // 상점 UI를 활성화하는 메서드
    void ToggleShop()
    {
        SetUIElementsState(false, false, true, true);
    }

    // 게임 종료를 처리하는 메서드
    void EndGame()
    {
        // 게임 종료 로직 추가
        Application.Quit();
    }

    // 전달된 액션을 실행하고 UI 상태를 변경하는 메서드
    void ToggleUI(System.Action action)
    {
        action.Invoke();
        mainButtonPanel.SetActive(false); // 메인 버튼 패널 비활성화
        invenButtonPanel.SetActive(true); // 인벤 버튼 패널 활성화
    }

    // 메인 버튼들을 다시 표시하는 메서드
    public void ShowMainButtons()
    {
        SetAllUIElementsActive(false);
        mainButtonPanel.SetActive(true);
        invenButtonPanel.SetActive(false);
    }

    // 모든 UI 요소의 활성 상태를 설정하는 헬퍼 메서드
    void SetAllUIElementsActive(bool state)
    {
        equop.gameObject.SetActive(state);
        inventory.gameObject.SetActive(state);
        shopInventory.gameObject.SetActive(state);
        worldInventory.gameObject.SetActive(state);
    }

    // 특정 UI 요소들의 활성 상태를 설정하는 헬퍼 메서드
    void SetUIElementsState(bool equopState, bool inventoryState, bool shopInventoryState, bool worldInventoryState)
    {
        equop.gameObject.SetActive(equopState);
        inventory.gameObject.SetActive(inventoryState);
        shopInventory.gameObject.SetActive(shopInventoryState);
        worldInventory.gameObject.SetActive(worldInventoryState);
    }
}
