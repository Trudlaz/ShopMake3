using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBase : ItemBase
{
    public int amount = 1;

    public override void Interact(ItemCode itemCode)
    {
        GameManager.Instance.InventoryUI.Inventory.AddItem(ItemCode.OneHundreadDol, amount);
        //GameManager.Instance.InventoryUI.Inventory.AddItem(ItemCode.OneThousandDol, amount);
        //GameManager.Instance.InventoryUI.Inventory.AddItem(ItemCode.TenThousandDol, amount);

        GameObject obj = GameManager.Instance.ItemData[itemCode].itemPrefab;
        Destroy(obj.gameObject);
    }
}
