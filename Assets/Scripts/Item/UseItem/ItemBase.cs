using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public ItemCode itemCode;
    public int Price = 1000;
    public float Weight = 3.0f;

    
    public virtual void Use()
    {

    }

    public virtual void UnUse()
    {

    }

    public virtual void Interact()
    {
        Debug.Log(itemCode);
        GameManager.Instance.InventoryUI.Inventory.AddItem(itemCode);
        Destroy(gameObject);
    }
}





