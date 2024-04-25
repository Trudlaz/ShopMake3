using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedWeapon : MonoBehaviour
{
    // 단축키 누르면 이미지에 해당 단축키에 맞는 부위의 슬롯 아이템 데이터 가져와서 보여주기
    Image weaponImage;

    private void Awake()
    {
        weaponImage = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        weaponImage = null;
        weaponImage.color = Color.clear;
    }

    void Refresh(Sprite weapon)
    {
        // 장비창에서 
        weaponImage.sprite = weapon;
        weaponImage.color = Color.white;
    }
}
