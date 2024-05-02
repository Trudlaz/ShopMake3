using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 인벤토리 및 장비창 열고 닫기용 클래스
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    DragSlotUI dragSlot;

    public DragSlotUI DragSlot => dragSlot;

    ResultButton_UI resultButton;

    public ResultButton_UI ResultButton => resultButton;


    private void Awake()
    {
        dragSlot = transform.GetComponentInChildren<DragSlotUI>();
        resultButton = transform.GetComponentInChildren<ResultButton_UI>();
    }

    public void Open()
    {
        resultButton.gameObject.SetActive(true);
    }

    public void Close()
    {
        resultButton.gameObject.SetActive(false);
    }




}
