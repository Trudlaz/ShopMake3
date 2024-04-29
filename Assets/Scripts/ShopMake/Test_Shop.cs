using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Shop : MonoBehaviour
{
    public ItemCode code = ItemCode.SmallHeal;
    ShopInventory shopInventory;    // 상점 인벤토리
    public ShopInventoryUI shopInventoryUI;
    WorldInventory worldInventory;  // 월드 인벤토리
    public WorldInventory_UI worldInventoryUI;
    public Player player;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is not set in Test_Shop.");
            return;
        }

        worldInventory = new WorldInventory(player);
        worldInventory.AddItem(ItemCode.SmallHeal);
        worldInventory.AddItem(ItemCode.MiddleHeal);
        worldInventory.AddItem(ItemCode.BigHeal);
        worldInventory.AddItem(ItemCode.SmallSpeed);
        worldInventory.AddItem(ItemCode.MiddleSpeed);
        worldInventory.AddItem(ItemCode.BigSpeed);
        worldInventory.AddItem(ItemCode.SmallStrength);
        worldInventory.AddItem(ItemCode.MiddleStrength);
        worldInventory.AddItem(ItemCode.BigStrength);

        // 인벤토리 클래스를 생성하고 초기화합니다.
        shopInventory = new ShopInventory();
        InitializeShopInventory();
    }

    void InitializeShopInventory()
    {
        // 상점 인벤토리에 아이템을 추가합니다.
        AddItemsToShop();

        if (worldInventoryUI == null)
        {
            Debug.LogError("WorldInventoryUI component is not assigned!");
            return; // WorldInventoryUI가 null인 경우 초기화 중단
        }

        if (worldInventory == null)
        {
            Debug.LogError("WorldInventory is not initialized!");
            return; // WorldInventory가 null인 경우 초기화 중단
        }

        // 월드 인벤토리 초기화
        worldInventoryUI.InitializeWorldInventory(worldInventory);

        if (shopInventoryUI == null)
        {
            Debug.LogError("ShopInventoryUI component is not assigned!");
            return; // ShopInventoryUI가 null인 경우 초기화 중단
        }

        // 상점 인벤토리 초기화
        shopInventoryUI.InitializeShop();
    }

    private void AddItemsToShop()
    {
        if (shopInventory == null)
        {
            Debug.LogError("ShopInventory is not initialized!");
            return;
        }

        // 상점 인벤토리에 아이템을 추가합니다.
        shopInventory.AddItem(ItemCode.Pistol);
        shopInventory.AddItem(ItemCode.Rifle);
        shopInventory.AddItem(ItemCode.Shotgun);
        shopInventory.AddItem(ItemCode.Sniper);
        shopInventory.AddItem(ItemCode.Key);
    }
}
