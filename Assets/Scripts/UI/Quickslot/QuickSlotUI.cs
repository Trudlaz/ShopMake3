using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotUI : MonoBehaviour
{
    public Image[] quickSlotImage = new Image[8];
    private QuickSlot quickSlot;

    private void Awake()
    {
        quickSlot = GetComponent<QuickSlot>();
    }

    public void QuickSlotImageChange(uint index)
    {
        quickSlotImage[index].sprite = quickSlot.Equip.slots[index].ItemData.itemImage;
    }
}
