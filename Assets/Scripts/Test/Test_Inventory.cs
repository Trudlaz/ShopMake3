using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Inventory : TestBase
{
    public ItemCode code = ItemCode.SmallHeal;

    [Range(0, 35)]
    public uint fromIndex = 0;

    [Range(0, 35)]
    public uint toIndex = 0;

    Inventory inven;

    public Inventory_UI inventoryUI;

    Player player;

#if UNITY_EDITOR
    private void Start()
    {
        player = GameManager.Instance.Player;

        // [ 루비(1/10), 사파이어(2/3), (빈칸), 에메랄드(3/5), (빈칸), (빈칸) ] 
        inven = new Inventory(player);
        inven.AddItem(ItemCode.SmallHeal);
        inven.AddItem(ItemCode.MiddleHeal);
        inven.AddItem(ItemCode.BigHeal);
        inven.AddItem(ItemCode.SmallSpeed);
        inven.AddItem(ItemCode.MiddleSpeed);
        inven.AddItem(ItemCode.BigSpeed);
        inven.AddItem(ItemCode.SmallStrength);
        inven.AddItem(ItemCode.MiddleStrength);
        inven.AddItem(ItemCode.BigStrength);
        inven.AddItem(ItemCode.ExplodeGrenade);
        inven.AddItem(ItemCode.FireGrenade);
        inven.AddItem(ItemCode.NoiseGrenade);
        inven.AddItem(ItemCode.BoomTrap);
        inven.AddItem(ItemCode.SlowTrap);
        inven.AddItem(ItemCode.StunTrap);
        inven.AddItem(ItemCode.Pistol);
        inven.AddItem(ItemCode.Rifle);
        inven.AddItem(ItemCode.Shotgun);
        inven.AddItem(ItemCode.Sniper);
        inven.AddItem(ItemCode.Key);
        inven.AddItem(ItemCode.PistolBullet);
        inven.AddItem(ItemCode.RifleBullet);
        inven.AddItem(ItemCode.ShotgunBullet);
        inven.AddItem(ItemCode.SniperBullet);
        inven.AddItem(ItemCode.OneHundreadDol);
        inven.AddItem(ItemCode.OneThousandDol);
        inven.AddItem(ItemCode.TenThousandDol);
        inven.Test_InventoryPrint();

        inventoryUI.InitializeInventory(inven);

        
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        // 추가
        inven.AddItem(code, fromIndex);
        inven.Test_InventoryPrint();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        // 삭제
        inven.RemoveItem(fromIndex);
        inven.Test_InventoryPrint();
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        // 이동
        inven.FindItem(ItemCode.Key);
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
