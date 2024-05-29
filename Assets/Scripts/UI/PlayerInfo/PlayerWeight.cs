using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWeight : MonoBehaviour
{
    TextMeshProUGUI current;
    TextMeshProUGUI max;

    Player player;
    float maxWeight;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
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
            max.text = $"{(maxWeight):f1}KG";

            current.text = "0";
        }

        player.onWeightChange += Refresh;
    }

    public void Refresh(float weight)
    {
        current.text = $"{(weight):f1}";
        if (weight > maxWeight)
        {
            current.color = Color.red;
        }
        else
        {
            current.color = Color.black;
        }

    }
}
