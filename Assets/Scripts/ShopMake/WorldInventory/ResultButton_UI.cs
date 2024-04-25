using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultButton_UI : MonoBehaviour
{
    Button resultButton;

    GameManager gameManager;

    private void Awake()
    {
        resultButton = GetComponent<Button>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;

        resultButton.onClick.AddListener(OnClick);

    }

    void OnClick()
    {
        gameManager.InventoryUI.InventoryResult();
    }
}
