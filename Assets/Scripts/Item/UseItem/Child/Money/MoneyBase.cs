using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBase : ItemBase
{
    public int amount = 1;

    public override void Interact()
    {
        GameManager.Instance.InventoryUI.Inventory.AddItem(ItemCode.OneHundreadDol, amount);
        Destroy(gameObject);
    }
}
