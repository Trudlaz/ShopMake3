using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// 게임 전반을 관리하는 싱글턴 클래스
public class GameManager : Singleton<GameManager>
{
    // 게임에 필요한 주요 컴포넌트 참조
    private Player player;
    private ItemDataManager itemDataManager;
    private Inventory_UI inventoryUI;
    private WorldInventory_UI worldInventoryUI;
    private InventoryManager inventoryManager;
    private TimeSystem timeSys;
    private WeaponBase weaponBase;
    private ShopInventoryUI shopInventoryUI;
    private Equip_UI equipUI;

    // 외부 접근을 위한 프로퍼티
    public Player Player => player;
    public ItemDataManager ItemData => itemDataManager;
    public Inventory_UI InventoryUI => inventoryUI;
    public WorldInventory_UI WorldInventoryUI => worldInventoryUI;
    public InventoryManager InventoryManager => inventoryManager;
    public TimeSystem TimeSystem => timeSys;
    public WeaponBase WeaponBase => weaponBase;
    public ShopInventoryUI ShopInventoryUI => shopInventoryUI;
    public Equip_UI EquipUI => equipUI;

    // 중요 씬 이름 상수화
    private const string InGameSceneName = "InGameScene";
    private const string MainMenuSceneName = "MainMenuScene";

    // 씬 로딩 완료와 게임 종료 이벤트
    public delegate void SceneAction();
    public event SceneAction OnGameStartCompleted;
    public event SceneAction OnGameEnding;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        LoadComponentReferences();
    }

    // 게임 컴포넌트 참조 로드
    private void LoadComponentReferences()
    {
        player = FindAnyObjectByType<Player>();
        itemDataManager = GetComponent<ItemDataManager>();
        inventoryUI = FindAnyObjectByType<Inventory_UI>();
        worldInventoryUI = FindAnyObjectByType<WorldInventory_UI>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        timeSys = FindAnyObjectByType<TimeSystem>();
        weaponBase = FindAnyObjectByType<WeaponBase>();
        shopInventoryUI = FindAnyObjectByType<ShopInventoryUI>();
        equipUI = FindObjectOfType<Equip_UI>();
    }

    // 게임 시작 시 InGameScene 로드
    public void StartGame(string v)
    {
        LoadScene(InGameSceneName, OnGameStartCompleted);

        // 메인메뉴씬에서 인게임씬으로 변경 전 이벤트를 호출
        OnGameStartCompleted?.Invoke();
    }

    // 게임 종료 시 MainMenuScene 로드
    public void EndGame(string v)
    {
        SaveInventory(); // 인벤토리 저장

        // 인게임씬에서 메인메뉴씬으로 변경 전 이벤트를 호출
        OnGameEnding?.Invoke();
        LoadScene(MainMenuSceneName, OnGameEnding);
    }

    // 지정된 씬을 비동기적으로 로드하고 완료 후 액션 처리
    private void LoadScene(string sceneName, SceneAction onLoaded)
    {
        Debug.Log($"Loading scene: {sceneName}");
        StartCoroutine(LoadAsyncScene(sceneName, onLoaded));
    }

    // 비동기 씬 로딩 처리 코루틴
    private IEnumerator LoadAsyncScene(string sceneName, SceneAction onLoaded)
    {
        if (sceneName == InGameSceneName)
        {
            SaveWorldInventory(); // 월드 인벤토리 저장
            SaveInventory(); // 인벤토리 저장
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading progress for {sceneName}: {asyncLoad.progress * 100}%");
            yield return null;
        }

        if (sceneName == MainMenuSceneName)
        {
            LoadWorldInventory(); // 월드 인벤토리 불러오기
            LoadInventory(); // 인벤토리 불러오기
        }
        else if (sceneName == InGameSceneName)
        {
            LoadInventory(); // 인벤토리 불러오기
        }

        if (onLoaded != null)
            onLoaded.Invoke();
    }

    // 데이터 저장 및 불러오기 관련 메서드
    public void SaveWorldInventory()
    {
        if (worldInventoryUI != null)
            worldInventoryUI.WorldInven.SaveInventoryToJson();
    }

    public void LoadWorldInventory()
    {
        if (worldInventoryUI != null)
            worldInventoryUI.WorldInven.LoadInventoryFromJson();
    }

    public void SaveInventory()
    {
        if (inventoryUI != null)
            inventoryUI.Inventory.SaveInventoryToJson();
    }

    public void LoadInventory()
    {
        if (inventoryUI != null)
            inventoryUI.Inventory.LoadInventoryFromJson();
    }
}
