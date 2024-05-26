using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class EquipSlotData
{
    public ItemCode ItemCode;
    public bool IsEquipped;
}

public class Equip
{
    private const int Default_Inventory_Size = 6;
    public EquipSlot[] slots;
    public EquipSlot this[uint index] => slots[index];
    private int SlotCount => slots.Length;
    private Player owner;
    private DragSlot dragSlot;
    private uint dragSlotIndex = 999999999;
    public DragSlot DragSlot => dragSlot;
    private ItemDataManager itemDataManager;
    public Player Owner => owner;
    private Equip_UI equipUI;
    public QuickSlot quickSlot;

    public Equip(GameManager owner, uint size = Default_Inventory_Size)
    {
        slots = new EquipSlot[size];

        for (uint i = 0; i < slots.Length; i++)
        {
            slots[i] = new EquipSlot(i);
        }

        dragSlot = new DragSlot(dragSlotIndex);
        itemDataManager = GameManager.Instance.ItemData;
        equipUI = GameManager.Instance.EquipUI;
        this.owner = owner.Player;

        Transform equipUIObject = equipUI.gameObject.transform.GetChild(0);

        for (uint i = 0; i < slots.Length; i++)
        {
            slots[i].slotType = equipUIObject.GetChild((int)i).GetComponent<EquipSlot_UI>().slotType;
        }
    }

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

    public bool AddItem(ItemCode code, uint slotIndex)
    {
        if (IsValidIndex(slotIndex))
        {
            ItemData data = itemDataManager[code];
            EquipSlot slot = slots[slotIndex];

            if (slot.slotType.Contains(data.itemType))
            {
                if (slot.IsEmpty)
                {
                    slot.AssignSlotItem(data);
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsValidIndex(uint index)
    {
        return index < SlotCount;
    }

    private bool IsValidIndex(ItemSlot slot)
    {
        return (slot != null) || (slot == dragSlot);
    }

    private void RemoveItem(uint slotIndex, uint count = 1)
    {
        if (IsValidIndex(slotIndex))
        {
            EquipSlot slot = slots[slotIndex];
            slot.DecreaseSlotItem(count);
        }
    }

    public void RemoveItem(ItemCode code, uint count = 1)
    {
        for (int i = 0; i < SlotCount; i++)
        {
            ItemData data = itemDataManager[code];
            EquipSlot slot = slots[i];

            if (slot.slotType.Contains(data.itemType))
            {
                if (!slot.IsEmpty)
                {
                    RemoveItem((uint)i, count);
                    break;
                }
            }
        }
    }

    public void ClearEquip()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
    }

    public void SaveEquipToJson()
    {
        List<EquipSlotData> slotDataList = new List<EquipSlotData>();
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty)
            {
                slotDataList.Add(new EquipSlotData()
                {
                    ItemCode = slot.ItemData.itemId,
                    IsEquipped = slot.IsEquiped
                });
            }
        }

        string json = JsonConvert.SerializeObject(slotDataList, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "equip.json"), json);
        Debug.Log("장비 데이터를 저장했습니다.");
    }

    public void LoadEquipFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "equip.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            List<EquipSlotData> slotDataList = JsonConvert.DeserializeObject<List<EquipSlotData>>(json);
            ClearEquip();  // 기존 장비 클리어

            foreach (var slotData in slotDataList)
            {
                ItemData itemData = itemDataManager.GetItemDataByCode(slotData.ItemCode);
                EquipSlot slot = GetEmptySlot(slotData.ItemCode);  // 빈 슬롯 찾기 메서드 구현 필요
                if (slot != null)
                {
                    slot.AssignSlotItem(itemData, 1, slotData.IsEquipped);  // 장비 슬롯에 아이템 할당
                }
            }
            Debug.Log("장비 데이터를 불러왔습니다.");
        }
        else
        {
            Debug.LogWarning("장비 데이터 파일을 찾을 수 없습니다.");
        }
    }

    private EquipSlot GetEmptySlot(ItemCode itemCode)
    {
        // 모든 슬롯을 순회하며 비어 있는 슬롯을 찾음
        foreach (var slot in slots)
        {
            if (slot.IsEmpty && slot.slotType.Contains(itemDataManager[itemCode].itemType))
            {
                return slot;
            }
        }
        return null; // 비어 있는 슬롯이 없으면 null 반환
    }
}
