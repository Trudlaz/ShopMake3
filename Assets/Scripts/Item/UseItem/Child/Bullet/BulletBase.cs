using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : ItemBase
{
    const int bulletCount = 5;

    public enum BulletType
    {
        Pistolbullet = 0,
        Riflebullet = 1,
        Shotgunbullet = 2,
        Sniperbullet = 3,
    }

    public override void Interact(ItemCode itemCode)
    {
        GameManager.Instance.InventoryUI.Inventory.AddItem(itemCode, 5);
        GameObject obj = GameManager.Instance.ItemData[itemCode].itemPrefab;
        Destroy(obj);
    }

}
