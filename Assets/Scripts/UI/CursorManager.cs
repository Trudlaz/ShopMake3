using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    InventoryManager inventoryManager;

    PauseMenu pauseMenu;

    PlayerInput input;
    PlayerMove inputMove;

    bool isInven;
    bool isPause;

    private void Awake()
    {
        input = new PlayerInput();
        inputMove = new PlayerMove();

        inventoryManager = GetComponentInChildren<InventoryManager>();
        pauseMenu = GetComponentInChildren<PauseMenu>();
    }

    private void Start()
    {
        CursorVisible(false);
    }

    private void OnEnable()
    {
        input.Pause.Enable();
        input.UI.Enable();

        input.Pause.Pause.performed += OnPause;
        input.UI.Inventory.performed += OnOpen;
    }

    private void OnDisable()
    {
        input.UI.Inventory.performed -= OnOpen;
        input.Pause.Pause.performed -= OnPause;

        input.Pause.Disable();
        input.UI.Disable();
    }

    private void OnOpen(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(!isInven && !isPause)
        {
            InvenOpen(true);
        }
        else
        {
            InvenOpen(false);
        }
    }

    private void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(!isPause && !isInven)
        {
            PauseOpen(true);
        }
        else if(!isPause && isInven)
        {
            InvenOpen(false);
        }
        else
        {
            PauseOpen(false);
        }
    }

    /// <summary>
    /// 마우스 커서를 보일지 안보일지 정하는 함수
    /// </summary>
    /// <param name="isVisible">true면 보이고 false면 안보임</param>
    void CursorVisible(bool isVisible)
    {
        Cursor.visible = isVisible;
        if (isVisible)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void InvenOpen(bool isOn)
    {
        if (isOn)
        {
            inventoryManager.Open();
            //inputMove.Disable();

            CursorVisible(true);
            Time.timeScale = 0.0f;
            isInven = true;
        }
        else
        {
            inventoryManager.Close();
            //inputMove.Enable();

            CursorVisible(false);
            Time.timeScale = 1.0f;
            isInven = false;
        }
    }


    public void PauseOpen(bool isOn)
    {
        if (isOn)
        {
            pauseMenu.Open();
            inputMove.Player.Disable();
            input.UI.Disable();

            CursorVisible(true);
            Time.timeScale = 0.0f;
            isPause = true;
        }
        else
        {
            pauseMenu.Close();
            inputMove.Player.Enable();
            input.UI.Enable();

            CursorVisible(false);
            Time.timeScale = 1.0f;
            isPause = false;
        }
    }




}
