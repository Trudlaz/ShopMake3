using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_InvenInterection : TestBase
{
    public ItemCode code = ItemCode.SmallHeal;

    [Range(0, 35)]
    public uint invenFromIndex = 0;

    [Range(0, 35)]
    public uint invenToIndex = 0;

    [Range(0, 143)]
    public uint worldFfromIndex = 0;

    [Range(0, 143)]
    public uint worldToIndex = 0;

    Inventory inven;

    public Inventory_UI inventoryUI;

    WorldInventory invenWorld;

    public WorldInventory_UI worldInventoryUI;

    Player player;

#if UNITY_EDITOR
    private void Start()
    {
        player = GameManager.Instance.Player;

        // [ 루비(1/10), 사파이어(2/3), (빈칸), 에메랄드(3/5), (빈칸), (빈칸) ] 
        inven = new Inventory(player);
        inventoryUI.InitializeInventory(inven);

        invenWorld = new WorldInventory(player);
        invenWorld.AddItem(ItemCode.SmallHeal);
        invenWorld.AddItem(ItemCode.MiddleHeal);
        invenWorld.AddItem(ItemCode.BigHeal);
        invenWorld.AddItem(ItemCode.SmallSpeed);
        invenWorld.AddItem(ItemCode.MiddleSpeed);
        invenWorld.AddItem(ItemCode.BigSpeed);
        invenWorld.AddItem(ItemCode.SmallStrength);
        invenWorld.AddItem(ItemCode.MiddleStrength);
        invenWorld.AddItem(ItemCode.BigStrength);
        invenWorld.AddItem(ItemCode.ExplodeGrenade);
        invenWorld.AddItem(ItemCode.FireGrenade);
        invenWorld.AddItem(ItemCode.NoiseGrenade);
        invenWorld.AddItem(ItemCode.BoomTrap);
        invenWorld.AddItem(ItemCode.SlowTrap);
        invenWorld.AddItem(ItemCode.StunTrap);
        invenWorld.AddItem(ItemCode.Pistol);
        invenWorld.AddItem(ItemCode.Rifle);
        invenWorld.AddItem(ItemCode.Shotgun);
        invenWorld.AddItem(ItemCode.Sniper);
        invenWorld.AddItem(ItemCode.Key);
        invenWorld.AddItem(ItemCode.PistolBullet);
        invenWorld.AddItem(ItemCode.RifleBullet);
        invenWorld.AddItem(ItemCode.ShotgunBullet);
        invenWorld.AddItem(ItemCode.SniperBullet);
        invenWorld.AddItem(ItemCode.OneHundreadDol);
        invenWorld.AddItem(ItemCode.OneThousandDol);
        invenWorld.AddItem(ItemCode.TenThousandDol);
        invenWorld.Test_InventoryPrint();

        worldInventoryUI.InitializeWorldInventory(invenWorld);


    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        // 추가
        inven.AddItem(code, invenFromIndex);
        inven.Test_InventoryPrint();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        // 삭제
        inven.RemoveItem(invenFromIndex);
        inven.Test_InventoryPrint();
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        // 이동
        inven.FindItem(ItemType.Key);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        // 인벤토리 정리
        inven.ClearInventory();
        // inven.Test_InventoryPrint();
    }
#endif
}

