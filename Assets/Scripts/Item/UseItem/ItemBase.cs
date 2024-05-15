using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public ItemCode itemCode;
    public int Price = 1000;
    public float Weight = 3.0f;

    public ItemCode GetItemCode()
    {
        return itemCode;
    }

    public void SetItemCode(ItemCode code)
    {
        itemCode = code;
    }
    public virtual void Use()
    {

    }

    public virtual void UnUse()
    {

    }

    public virtual void Interact(ItemCode itemCode)
    {
        GameManager.Instance.InventoryUI.Inventory.AddItem(itemCode);
        GameObject obj = GameManager.Instance.ItemData[itemCode].itemPrefab;
       // Destroy(obj.gameObject);
    }
}





