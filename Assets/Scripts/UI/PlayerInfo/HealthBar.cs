using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Color color = Color.white;

    Slider slider;
    Player player;

    TextMeshProUGUI hpText;
    TextMeshProUGUI maxHpText;


    private void Awake()
    {
        slider = GetComponent<Slider>();

        Transform child = transform.GetChild(2);
        hpText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(4);
        maxHpText = child.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        player = GameManager.Instance.Player;

        maxHpText.text = player.MaxHp.ToString();
        hpText.text = player.Hp.ToString("f0");
        
        player.onHealthChange += Refresh;
    }

    public void Refresh(float ratio)
    {
        slider.value = ratio / player.MaxHp;
        hpText.text = $"{(ratio):f0}";
    }
}
