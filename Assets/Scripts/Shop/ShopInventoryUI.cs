using System;
using UnityEngine;

public class ShopInventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;       // 슬롯 프리팹
    public Transform slotParent;        // 슬롯이 생성될 부모 Transform
    private ShopInventory shopInventory; // 상점 인벤토리

    private Slot_UI[] shopSlots;        // 상점 슬롯 UI 배열

    private void Awake()
    {
        // 상점 인벤토리 컴포넌트 참조
        shopInventory = GetComponent<ShopInventory>();
        // 슬롯 초기화 함수 호출
        InitializeSlots();
    }

    // 슬롯 초기화
    private void InitializeSlots()
    {
        // 슬롯 UI 배열 생성
        shopSlots = new Slot_UI[shopInventory.Items.Count];

        // 상점 아이템들에 대한 슬롯을 생성하고 초기화
        for (int i = 0; i < shopInventory.Items.Count; i++)
        {
            // 슬롯 프리팹을 복제하여 새로운 슬롯 생성
            GameObject slotObject = Instantiate(slotPrefab, slotParent);
            // 슬롯 UI 컴포넌트 참조
            Slot_UI shopSlot = slotObject.GetComponent<Slot_UI>();

            if (shopSlot != null)
            {
                // 슬롯 UI를 배열에 할당
                shopSlots[i] = shopSlot;
                // 우클릭 이벤트 리스너 연결
                int index = i; // 클로저를 사용하여 캡처하기 위해 index 변수를 따로 생성
                shopSlot.onRightClick += HandleRightClick;
            }
        }
    }

    // 우클릭 이벤트 처리 함수
    private void HandleRightClick(uint obj)
    {
        // 우클릭 시 실행될 로직
        Debug.Log($"{obj}");
        // 여기에서 index를 사용하여 해당 슬롯에 대한 데이터 또는 처리를 수행
    }
}
