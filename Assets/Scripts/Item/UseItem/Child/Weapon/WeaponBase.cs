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

    public GameObject bulletEffect;
    private ParticleSystem ps;

    public int CurrentAmmo
    {
        get => currentAmmo;
        set
        {
            if (currentAmmo != value)
            {
                currentAmmo = value;
                onAmmoChange?.Invoke(currentAmmo, maxAmmo);
            }
        }
    }
    int currentAmmo = 0;

    float coolTime = 0f;

    public bool canFire => coolTime < fireRate && currentAmmo > 0;
    public Action<ItemCode, int> onReload;    //장비창에 장착될때 인벤토리의 리로딩 함수와 연결 
    public Action<int, int> onAmmoChange;

    private void Update()
    {
        coolTime -= Time.deltaTime;
    }

    // Player_UI관련 -----------------------------------------------------

    private void OnEnable()
    {
        Player player = GetComponentInParent<Player>();
        if (player != null)
        {
            PlayerUI playerUI = GameManager.Instance.PlayerUI;
            onAmmoChange += playerUI.Remain.Refresh;

            playerUI.Remain.Refresh(currentAmmo, maxAmmo);
        }
    }

    private void OnDisable()
    {
        Player player = GetComponentInParent<Player>();
        if (player != null)
        {
            PlayerUI playerUI = GameManager.Instance.PlayerUI;

            playerUI.Remain.Refresh(0, 0);

            onAmmoChange = null;
        }
    }

    // ------------------------------------------------------------------------

    public override void Use() //리로딩
    {
        int needAmmo = maxAmmo - CurrentAmmo;
        ItemCode needType = ItemCode.PistolBullet;
        switch (ammunitionType)
        {
            case BulletType.Pistolbullet:
                needType = ItemCode.PistolBullet;
                break;
            case BulletType.Riflebullet:
                needType = ItemCode.RifleBullet;
                break;
            case BulletType.Sniperbullet:
                needType = ItemCode.SniperBullet;
                break;
            case BulletType.Shotgunbullet:
                needType = ItemCode.ShotgunBullet;
                break;
        }
        onReload?.Invoke(needType, needAmmo);
    }

    public void Reload(int ammo)
    {
        CurrentAmmo += ammo;
    }

    public virtual void Fire()
    {
        if (canFire)
        {
            CurrentAmmo--;
            coolTime = fireRate;

            // Raycast 및 이펙트 처리
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hitInfo in hits)
            {
                if (hitInfo.transform.gameObject.CompareTag("Item"))
                {
                    continue; // 'Item' 태그를 가진 오브젝트는 무시
                }

                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    // Enemy 레이어를 가진 오브젝트에 데미지를 주고 이펙트를 생성
                    EnemyBace eFSM = hitInfo.transform.gameObject.GetComponent<EnemyBace>();
                    if (eFSM != null)
                    {
                        eFSM.Demage(damage);
                    }
                    if (bulletEffect != null && ps != null)
                    {
                        bulletEffect.transform.position = hitInfo.point;
                        ps.Play();
                    }
                    break; // 적과 충돌한 경우 루프 종료
                }
                else
                {
                    // 첫 번째 적이 아닌 충돌 지점에 이펙트를 생성
                    if (bulletEffect != null && ps != null)
                    {
                        bulletEffect.transform.position = hitInfo.point;
                        ps.Play();
                    }
                    break; // 충돌 처리 후 루프 종료
                }
            }
        }
        else
        {
            if (currentAmmo <= 0)
            {
                Debug.Log("탄약이 부족합니다.");
            }
            else if (coolTime >= fireRate)
            {
                Debug.Log("쿨다운 중입니다.");
            }
        }
    }


    // bulletEffect와 ps를 초기화하는 메서드 추가
    public void InitializeEffects(GameObject bulletEffect, ParticleSystem ps)
    {
        this.bulletEffect = bulletEffect;
        this.ps = ps;
    }
}
