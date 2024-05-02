using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellItemUI : MonoBehaviour
{
    ItemSlot targetSlot;

    Image icon;
    TMP_InputField inputField;
    Slider slider;

    const uint MinItemCount = 0;
    uint MaxItemCount => targetSlot.IsEmpty ? MinItemCount : (targetSlot.ItemCount);

    uint sellCount = 0;

    public uint SellCount
    {
        get => sellCount;
        set
        {
            if (sellCount != value)
            {
                sellCount = Math.Clamp(value, MinItemCount, MaxItemCount);
                inputField.text = sellCount.ToString();
                slider.value = sellCount;
            }
        }
    }

    public Action<ItemSlot, uint> onSellOK;


    private void Awake()
    {
        Transform child = transform.GetChild(0);
        icon = child.GetComponent<Image>();

        child = transform.GetChild(1);
        inputField = child.GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener((text) =>
        {
            if (uint.TryParse(text, out uint value))
            {
                SellCount = value;
            }
            else
            {
                SellCount = MinItemCount;
            }
        });

        child = transform.GetChild(2);
        slider = child.GetComponent<Slider>();
        slider.onValueChanged.AddListener((value) =>
        {
            SellCount = (uint)value;
        });

        child = transform.GetChild(3);
        Button plus = child.GetComponent<Button>();
        plus.onClick.AddListener(() =>
        {
            SellCount++;
        });
        child = transform.GetChild(4);
        Button minus = child.GetComponent<Button>();
        minus.onClick.AddListener(() =>
        {
            if (SellCount > 0)
                SellCount--;
        });
        child = transform.GetChild(5);
        Button ok = child.GetComponent<Button>();
        ok.onClick.AddListener(() =>
        {
            onSellOK?.Invoke(targetSlot, SellCount);
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
            SellCount = targetSlot.ItemCount / 2;

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
