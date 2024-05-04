using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    // 게임에 필요한 주요 컴포넌트들을 관리하는 변수들
    private Player player;
    private ItemDataManager itemDataManager;
    private Inventory_UI inventoryUI;
    private WorldInventory_UI worldInventoryUI;
    private TimeSystem timeSys;
    private WeaponBase weaponBase;
    private ShopInventoryUI shopInventoryUI; // 새롭게 추가된 샵 인벤토리 UI
    private Equip_UI equipUI;

    // 게임 씬 로딩 완료와 게임 종료 신호를 위한 델리게이트 및 이벤트
    public delegate void SceneAction();
    public event SceneAction OnGameStartCompleted; // 게임 시작 후 호출될 이벤트
    public event SceneAction OnGameEnding; // 게임 종료 전 호출될 이벤트

    // 외부 접근을 위한 프로퍼티 선언
    public Player Player => player;
    public ItemDataManager ItemData => itemDataManager;
    public Inventory_UI InventoryUI => inventoryUI;
    public WorldInventory_UI WorldInventory_UI => worldInventoryUI;
    public TimeSystem TimeSystem => timeSys;
    public WeaponBase WeaponBase => weaponBase;
    public ShopInventoryUI ShopInventoryUI => shopInventoryUI;
    public Equip_UI EquipUI => equipUI;

    // 게임 시작 시 호출되는 메소드, 지정된 씬을 비동기적으로 로드
    public void StartGame(string sceneName)
    {
        StartCoroutine(LoadGameStartScene(sceneName));
    }

    // 게임 종료 시 호출되는 메소드, 지정된 씬을 비동기적으로 로드
    public void EndGame(string sceneName)
    {
        OnGameEnding?.Invoke(); // 게임 종료 신호 이벤트 발생
        StartCoroutine(LoadGameEndScene(sceneName));
    }

    // 게임 시작 씬 로딩을 위한 코루틴
    private IEnumerator LoadGameStartScene(string sceneName)
    {
        yield return StartCoroutine(LoadAsyncScene(sceneName)); // 실제 씬 로딩 처리
        OnGameStartCompleted?.Invoke(); // 씬 로딩 완료 후 이벤트 발생
    }

    // 게임 종료 씬 로딩을 위한 코루틴
    private IEnumerator LoadGameEndScene(string sceneName)
    {
        yield return StartCoroutine(LoadAsyncScene(sceneName)); // 실제 씬 로딩 처리
    }

    // 비동기 씬 로딩 처리를 위한 코루틴
    private IEnumerator LoadAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%"); // 로딩 진행 상태 로깅
            yield return null;
        }
    }

    // 게임 초기화 시 필요한 컴포넌트를 찾아서 할당
    protected override void OnInitialize()
    {
        base.OnInitialize(); // 상속받은 Singleton의 초기화를 먼저 호출
        player = FindAnyObjectByType<Player>();
        inventoryUI = FindAnyObjectByType<Inventory_UI>();
        worldInventoryUI = FindAnyObjectByType<WorldInventory_UI>();
        timeSys = FindAnyObjectByType<TimeSystem>();
        weaponBase = FindAnyObjectByType<WeaponBase>();
        shopInventoryUI = FindAnyObjectByType<ShopInventoryUI>();
        equipUI = FindObjectOfType<Equip_UI>();
    }

    // 초기화 이전에 필요한 컴포넌트를 미리 설정
    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>(); // 아이템 데이터 관리자 컴포넌트 찾기
    }
}
