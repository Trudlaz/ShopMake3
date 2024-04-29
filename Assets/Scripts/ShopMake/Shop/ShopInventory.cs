public class ShopInventory
{
    private static readonly int Default_Inventory_Size = 144;
    private ItemSlot[] items;
    private ItemDataManager itemDataManager;
    private ShopInventoryUI shopInvenUI;

    public ShopInventory()
    {
        InitializeInventory(Default_Inventory_Size);
    }

    private void InitializeInventory(int size)
    {
        items = new ItemSlot[size];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new ItemSlot((uint)i);
        }
        itemDataManager = GameManager.Instance.ItemData; // GameManager를 적절히 참조하는 방법 필요
        shopInvenUI = GameManager.Instance.ShopInventoryUI; // 마찬가지로 적절한 참조 필요
    }

    public void AddItem(ItemCode code)
    {
        ItemData data = itemDataManager[code];
        if (data != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].IsEmpty)
                {
                    items[i].AssignSlotItem(data);
                    UpdateUI();
                    break;
                }
                else if (items[i].ItemData == data && items[i].SetSlotCount(out uint _))
                {
                    UpdateUI();
                    break;
                }
            }
        }
    }

    private void UpdateUI()
    {
        if (shopInvenUI != null)
        {
            shopInvenUI.UpdateSlots(items);
        }
    }
}
