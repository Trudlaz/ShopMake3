using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    Button reGameButton;
    Button quitGameButton;

    Transform child;

    CursorManager cursor;

    private void Awake()
    {
        cursor = GetComponentInParent<CursorManager>();

        child = transform.GetChild(0);
        Transform childButton;

        childButton = child.GetChild(0);
        reGameButton = childButton.GetComponent<Button>();
        reGameButton.onClick.AddListener(() =>
        {
            cursor.PauseOpen(false);
        });

        childButton = child.GetChild(1);
        quitGameButton = childButton.GetComponent<Button>();
    }
    private void Start()
    {
        Player player = GameManager.Instance.Player;
        quitGameButton.onClick.AddListener(() =>
        {
            cursor.PauseOpen(false);
            player.Die();
        });

        Close();
    }

    public void Open()
    {
        child.gameObject.SetActive(true);
    }

    public void Close()
    {
        child.gameObject.SetActive(false);
    }
}
