public class GameManager : Singleton<GameManager>
{
    // 플레이어, 아이템 데이터 관리자, 여러 인벤토리 UI 변수 선언
    private Player player;
    private ItemDataManager itemDataManager;
    private Inventory_UI inventoryUI;
    private WorldInventory_UI worldInventoryUI;
    private TimeSystem timeSys;
    private WeaponBase weaponBase;
    private ShopInventoryUI shopInventoryUI; // 샵 인벤토리 UI 추가
    private Equip_UI equipUI;

    // 플레이어, 아이템 데이터 관리자, 여러 인벤토리 UI 속성 정의
    public Player Player => player;
    public ItemDataManager ItemData => itemDataManager;
    public Inventory_UI InventoryUI => inventoryUI;
    public WorldInventory_UI WorldInventory_UI => worldInventoryUI;
    public TimeSystem TimeSystem => timeSys;
    public WeaponBase WeaponBase => weaponBase;
    public ShopInventoryUI ShopInventoryUI => shopInventoryUI; // 샵 인벤토리 UI에 대한 프로퍼티 추가
    public Equip_UI EquipUI => equipUI;

    // 초기화 단계에서 플레이어, 아이템 데이터 관리자, 인벤토리 UI 초기화
    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        inventoryUI = FindAnyObjectByType<Inventory_UI>();
        worldInventoryUI = FindAnyObjectByType<WorldInventory_UI>();
        timeSys = FindAnyObjectByType<TimeSystem>();
        weaponBase = FindAnyObjectByType<WeaponBase>();
        shopInventoryUI = FindAnyObjectByType<ShopInventoryUI>(); // 샵 인벤토리 UI 초기화
        equipUI = FindObjectOfType<Equip_UI>();
    }

    // 아이템 데이터 관리자 초기화 전에 호출되는 초기화 단계
    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>();
    }
}
