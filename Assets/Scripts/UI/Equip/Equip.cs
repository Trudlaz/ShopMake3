using UnityEngine;

// Equip 클래스는 아이템 장비 관리를 담당
public class Equip : MonoBehaviour
{
    private const int Default_Inventory_Size = 8;
    public EquipSlot[] slots;
    public EquipSlot this[uint index] => slots[index];
    private int SlotCount => slots.Length;
    private Player owner;
    private DragSlot dragSlot;
    private uint dragSlotIndex = 999999999;
    public DragSlot DragSlot => dragSlot;
    ItemDataManager itemDataManager;
    public Player Owner => owner;
    private Equip_UI equipUI;

    // 생성자
    public Equip(Player owner, GameObject slotsParent, uint size = Default_Inventory_Size)
    {
        slots = new EquipSlot[size];
        for (uint i = 0; i < slots.Length; i++)
        {
            slots[i] = new EquipSlot(i);
            slots[i].slotType = slotsParent.GetComponentsInChildren<EquipSlot_UI>()[i].slotType;
        }

        dragSlot = new DragSlot(dragSlotIndex);
        itemDataManager = GameManager.Instance.ItemData;
        equipUI = GameManager.Instance.EquipUI;
        this.owner = owner;
    }

    // 아이템을 장비하는 함수, 장비할 아이템 코드를 받아 처리
    public bool AddItem(ItemCode code)
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (AddItem(code, (uint)i))
            {
                return true;
            }
        }

        return false;
    }

    // 슬롯에 아이템을 추가하는 함수, 실패할 경우 false 반환
    public bool AddItem(ItemCode code, uint slotIndex)
    {
        bool result = false;

        if (IsValidIndex(slotIndex))
        {
            ItemData data = itemDataManager[code];
            EquipSlot slot = slots[slotIndex];

            if (slot.slotType.Contains(data.itemType))
            {
                if (slot.IsEmpty)
                {
                    slot.AssignSlotItem(data);
                    result = true;
                }
            }
        }

        return result;
    }

    // 아이템 이동 함수, 두 아이템 슬롯을 받아 처리
    public void MoveItem(ItemSlot from, ItemSlot to)
    {
        uint fromIndex = from.Index; // 'from' 슬롯의 인덱스
        uint toIndex = to.Index; // 'to' 슬롯의 인덱스

        // from과 to가 다른지, 그리고 유효한 인덱스를 가지고 있는지 확인
        if ((fromIndex != toIndex) && IsValidIndex(fromIndex) && IsValidIndex(toIndex))
        {
            SwapSlot(from, to);
        }
    }


    // 두 슬롯의 아이템을 교체하는 함수
    public void SwapSlot(ItemSlot slotA, ItemSlot slotB)
    {
        ItemData dragData = slotA.ItemData;
        uint dragCount = slotA.ItemCount;

        slotA.AssignSlotItem(slotB.ItemData, slotB.ItemCount);
        slotB.AssignSlotItem(dragData, dragCount);
    }

    // 인덱스 유효성 검사
    private bool IsValidIndex(uint index)
    {
        return index < SlotCount;
    }

    // 아이템 제거 함수
    public void RemoveItem(uint slotIndex, uint count = 1)
    {
        if (IsValidIndex(slotIndex))
        {
            EquipSlot slot = slots[slotIndex];
            slot.DecreaseSlotItem(count);
        }
    }

    // 장비 초기화
    public void ClearEquip()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
    }

    // 게임 오버 시 장비 비우기
    public void GameOver()
    {
        ClearEquip();
    }
}
