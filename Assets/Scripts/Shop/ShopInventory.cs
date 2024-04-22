using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    [SerializeField]
    private List<ItemSlot> items = new List<ItemSlot>(); // 상점에서 판매할 아이템 목록

    private ItemDataManager itemDataManager;
    private ShopInventoryUI shopInven;

    public List<ItemSlot> Items => items; // 외부 접근을 위한 프로퍼티

    void Awake()
    {
        // GameManager로부터 데이터 매니저와 UI 컴포넌트 가져오기
        itemDataManager = GameManager.Instance.ItemData;
        shopInven = GameManager.Instance.ShopInventoryUI; // 이 부분은 ShopInventory_UI가 있다고 가정
    }

    public void AddItem(ItemData itemData)
    {
        // 새 아이템을 인벤토리에 추가
        items.Add(new ItemSlot(itemData));
    }

    public void RemoveItem(int index)
    {
        // 인덱스에 따라 아이템 제거
        if (IsValidIndex(index))
        {
            items.RemoveAt(index);
        }
        else
        {
            Debug.LogError("Invalid index: " + index);
        }
    }

    public bool IsValidIndex(int index)
    {
        // 인덱스가 유효한 범위 내에 있는지 확인
        return index >= 0 && index < items.Count;
    }

    public ItemSlot GetItemSlot(int index)
    {
        // 유효한 인덱스면 해당 아이템 슬롯 반환, 그렇지 않으면 null 반환
        return IsValidIndex(index) ? items[index] : null;
    }
}
