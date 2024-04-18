using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeightPanel_UI : MonoBehaviour
{
    TextMeshProUGUI current;
    TextMeshProUGUI max;

    Player player;
    float maxWeight;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        current = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        max = child.GetComponent<TextMeshProUGUI>();

        

    }

    private void Start()
    {
        player = GameManager.Instance.Player;
        if (player != null)
        {
            maxWeight = player.MaxWeight;
            max.text = $" / {maxWeight}KG";

            // 나중에 장비창 만든 이후 총 무게로 바꿀 것!!
            // 플레이어를 찾지를 못해서 0이되네 왜지;;
        }
    }

    public void Refresh(int weight)
    {
        current.text = $"{weight}";
        if(weight > maxWeight)
        {
            current.color = Color.red;
        }
        else
        {
            current.color = Color.black;
        }

    }
}
