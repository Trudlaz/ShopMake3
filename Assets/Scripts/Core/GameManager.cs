public class GameManager : Singleton<GameManager>
{
    // 플레이어, 아이템 데이터 관리자, 여러 인벤토리 UI 변수 선언
    private Player player;
    private ItemDataManager itemDataManager;
    private Inventory_UI inventoryUI;
    private WorldInventory_UI worldInventoryUI;
    private ShopInventoryUI shopInventoryUI; // 샵 인벤토리 UI 추가
    private TimeSystem timeSys;

    // 플레이어, 아이템 데이터 관리자, 여러 인벤토리 UI 속성 정의
    public Player Player => player;
    public ItemDataManager ItemData => itemDataManager;
    public Inventory_UI InventoryUI => inventoryUI;
    public WorldInventory_UI WorldInventory_UI => worldInventoryUI;
    public ShopInventoryUI ShopInventoryUI => shopInventoryUI; // 샵 인벤토리 UI에 대한 프로퍼티 추가
    public TimeSystem TimeSystem => timeSys;

    // 초기화 단계에서 플레이어, 아이템 데이터 관리자, 인벤토리 UI 초기화
    protected override void OnInitialize()
    {
        base.OnInitialize(); // base 호출을 확실히 기입
        player = FindAnyObjectByType<Player>();
        inventoryUI = FindAnyObjectByType<Inventory_UI>();
        worldInventoryUI = FindAnyObjectByType<WorldInventory_UI>();
        shopInventoryUI = FindAnyObjectByType<ShopInventoryUI>(); // 샵 인벤토리 UI 초기화
        timeSys = FindAnyObjectByType<TimeSystem>();
    }

    // 아이템 데이터 관리자 초기화 전에 호출되는 초기화 단계
    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>();
    }
}
