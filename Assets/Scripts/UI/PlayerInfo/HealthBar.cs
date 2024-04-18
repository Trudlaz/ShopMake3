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

        maxHpText.text = player.maxHp.ToString();
        hpText.text = player.Hp.ToString("f0");
        slider.value = player.Hp / player.maxHp;
        player.OnHealthChange += Refresh;
    }

    public void Refresh(float ratio)
    {
        //slider.value = player.HP
        ratio = Mathf.Clamp01(ratio);
        slider.value = ratio;
        hpText.text = $"{(ratio * player.maxHp):f0}";
    }
}
