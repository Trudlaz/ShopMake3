using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;


// 인벤토리 및 장비창 열고 닫기용 클래스
public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// 플레이어가 탈출하고 아이템 정산이 끝나면 누르는 버튼
    /// </summary>
    Button resultButton;

    DragSlotUI dragSlot;
    Inventory_UI inven;
    Equip_UI equip;

    public DragSlotUI DragSlot => dragSlot;

    private void Awake()
    {
        dragSlot = GetComponentInChildren<DragSlotUI>();

        inven = GetComponentInChildren<Inventory_UI>();
        equip = GetComponentInChildren<Equip_UI>();

        Transform child = transform.GetChild(4);
        resultButton = child.GetComponent<Button>();
    }
    private void Start()
    {
        GameManager.Instance.OnGameEnding += () =>
        {
            resultButton.gameObject.SetActive(true);
        };

        resultButton.onClick.AddListener(OnClick);
        resultButton.gameObject.SetActive(false);
    }

    private void OnClick()
    {
        if (inven != null)
        {
            inven.InventoryResult();
        }
        resultButton.gameObject.SetActive(false);
    }

    public void Open()
    {
        inven.Open();
        equip.Open();

        Debug.Log("인벤토리 켜짐");
    }

    public void Close()
    {
        inven.Close();
        equip.Close();

        Debug.Log("인벤토리 꺼짐");
    }
}
