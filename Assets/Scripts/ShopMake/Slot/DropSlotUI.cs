using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropSlotUI : MonoBehaviour
{
    ItemSlot targetSlot;

    Image icon;
    TMP_InputField inputField;
    Slider slider;


    const uint MinItemCount = 0;
    uint MaxItemCount => targetSlot.IsEmpty ? MinItemCount : (targetSlot.ItemCount);

    uint dropCount = 0;

    public uint DropCount
    {
        get => dropCount;
        set
        {
            if(dropCount != value)
            {
                dropCount = Math.Clamp(value, MinItemCount, MaxItemCount);
                inputField.text = dropCount.ToString();
                slider.value = dropCount;
            }
        }
    }

    public Action<uint, uint> onDropOk;


    private void Awake()
    {
        Transform child = transform.GetChild(0);
        icon = child.GetComponent<Image>();

        child = transform.GetChild(1);
        inputField = child.GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener((text) =>
        {
            if ( uint.TryParse(text, out uint value) )
            {
                DropCount = value;
            }
            else
            {
                DropCount = MinItemCount;
            }
        });

        child = transform.GetChild(2);
        slider = child.GetComponent<Slider>();
        slider.onValueChanged.AddListener((value) =>
        {
            DropCount = (uint)value;
        });

        child = transform.GetChild(3);
        Button plus = child.GetComponent<Button>();
        plus.onClick.AddListener(() =>
        {
            DropCount++;
        });
        child = transform.GetChild(4);
        Button minus = child.GetComponent<Button>();
        minus.onClick.AddListener(() =>
        {
            if(DropCount >0)
            DropCount--;
        });
        child = transform.GetChild(5);
        Button ok = child.GetComponent<Button>();
        ok.onClick.AddListener(() =>
        {
            onDropOk?.Invoke(targetSlot.Index, DropCount);
            Close();
        });
        child = transform.GetChild(6);
        Button close = child.GetComponent<Button>();
        close.onClick.AddListener(() =>
        {
            Close();
        });
    }

    private void Start()
    {
        inputField.text = "0";
    }

    public bool Open(ItemSlot target)
    {
        bool result = false;
        if (!target.IsEmpty && target.ItemCount > MinItemCount)
        {
            targetSlot = target;
            icon.sprite = target.ItemData.itemImage;
            slider.minValue = MinItemCount;
            slider.maxValue = MaxItemCount;
            DropCount = targetSlot.ItemCount / 2;

            result = true;
            gameObject.SetActive(true);
        }
        return result;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }


}
