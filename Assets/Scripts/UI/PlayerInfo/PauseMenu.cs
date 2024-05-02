using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    Button reGameButton;
    Button quitGameButton;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        reGameButton = child.GetComponent<Button>();
        reGameButton.onClick.AddListener(Close);

        child = transform.GetChild(1);
        quitGameButton = child.GetComponent<Button>();
    }
    private void Start()
    {
        Player player = GameManager.Instance.Player;
        quitGameButton.onClick.AddListener( () =>
        {
            player.Die();
        });

        Close();
    }

    public void Open()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
