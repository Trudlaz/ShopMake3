using System;
using UnityEngine;

public class ShopInventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;
    private ShopInventory shopInventory;

    private Slot_UI[] shopSlots;

    private void Awake()
    {
        shopInventory = GetComponent<ShopInventory>();
        InitializeSlots();
    }

    private void InitializeSlots()
    {
        // 슬롯 UI 배열 생성
        shopSlots = new Slot_UI[shopInventory.Items.Count];

        for (int i = 0; i < shopInventory.Items.Count; i++)
        {
            GameObject slotObject = Instantiate(slotPrefab, slotParent);
            Slot_UI shopSlot = slotObject.GetComponent<Slot_UI>();

            if (shopSlot != null)
            {
                // 아이템 데이터만 전달
                shopSlots[i] = shopSlot;

                // 우클릭 이벤트 리스너 연결
                int index = i; // 클로저를 사용하여 캡처하기 위해 index 변수를 따로 생성
                shopSlot.onRightClick += HandleRightClick;
            }
        }
    }

    private void HandleRightClick(uint obj)
    {
        // 우클릭 시 실행될 로직
        Debug.Log($"Right-clicked on item slot {obj}");
        // 여기에서 index를 사용하여 해당 슬롯에 대한 데이터 또는 처리를 수행할 수 있습니다.
    }
}
