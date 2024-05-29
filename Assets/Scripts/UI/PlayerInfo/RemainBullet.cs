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

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        current = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        max = child.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 현재 사용 가능한 총알수
    /// </summary>
    /// <param name="currentAmmo"></param>
    public void Refresh(int currentAmmo, int maxAmmo) 
    {
        current.text = currentAmmo.ToString();
        max.text = maxAmmo.ToString();
    }
    private void Start()
    {
        current.text = "0";
        max.text = "0";
    }
}
