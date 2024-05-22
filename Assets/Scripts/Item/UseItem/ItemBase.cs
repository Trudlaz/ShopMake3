using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public ItemCode itemCode;
    public int Price = 1000;
    public float Weight = 3.0f;

    protected Player player;

    private void Start()
    {
        player = GameManager.Instance.Player;

    }

    public virtual void Use()
    {
    }

    public virtual void UnUse()
    {

    }

    public virtual void Interact()
    {
        GameManager.Instance.InventoryUI.Inventory.AddItem(itemCode);
        Destroy(gameObject);
    }
}





