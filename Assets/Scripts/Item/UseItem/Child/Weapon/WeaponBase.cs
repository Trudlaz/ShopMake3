using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletBase;

public class WeaponBase : ItemBase
{
    [Tooltip("최대 총알 수, 장탄수")]
    public int maxAmmo = 10;
    [Tooltip("연사력")]
    public float fireRate = 0.1f;
    [Tooltip("무게")]
    public float weight = 0f;
    [Tooltip("가격")]
    public uint price = 0;
    [Tooltip("내구도")]
    public float durability = 0f;
    [Tooltip("반동. 기본적 수치.")]
    public float recoil = 0f;
    [Tooltip("조준 거리")]
    public uint sightingRange = 0;
    [Tooltip("탄종")]
    public BulletType ammunitionType;
    [Tooltip("데미지")]
    public float damage = 5.0f;
    [Tooltip("최대 공격력, 치뎀")]
    public float headDamage = 10.0f;
    [Tooltip("명중률")]
    public float accuracy = 0f;
    [Tooltip("치확")]
    public float critRate = 0f;
    [Tooltip("탄속")]
    public uint muzzleVelocity = 0;
    [Tooltip("소음")]
    public float noiseVelocity = 7.0f;

    int currentAmmo = 0;
    float coolTime = 0f;
    public bool canFire => coolTime < fireRate && currentAmmo > 0;
    public Action<BulletType, int> onReload;    //장비창에 장착될때 인벤토리의 리로딩 함수와 연결 

    private void Update()
    {
        coolTime -= Time.deltaTime;
    }

    public override void Use() //리로딩
    {
        onReload?.Invoke(ammunitionType, maxAmmo);
    }

    public void ReLoad(int ammo)
    {
        currentAmmo = ammo;
    }

    public virtual void Fire() 
    {
        if (canFire) 
        {
            currentAmmo--;
            coolTime = fireRate;
        }
    }

}
