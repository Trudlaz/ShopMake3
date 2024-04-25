using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemainBullet : MonoBehaviour
{

    // 장비창에서 무기슬롯에 들어간 총기 가져오고 (안되면 인벤토리에서 가져오기)
    // 총기랑 맞는 클래스에서 최대 총알 수 가져오고 당장은 WeaponBase에서 가져오기

    // 인벤토리에서 장전한 총알 수 가져와서 텍스트로 보여주기

    TextMeshProUGUI current;
    TextMeshProUGUI max;

    WeaponBase weapon;
    Inventory_UI inventory;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        current = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        max = child.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        weapon = GameManager.Instance.WeaponBase;
        inventory = GameManager.Instance.InventoryUI;

        if (inventory != null)
        {
            inventory.Inventory.onReload += Refresh;
        }

        current.text = "-";
        max.text = "-";

        if (weapon != null)
        {
            max.text = weapon.maxAmmo.ToString();   // 나중에 총알관련 클래스 완성되면 넣어주기
        }
    }

    public void Refresh(int bullet) 
    {
        current.text = bullet.ToString();
    }
}
