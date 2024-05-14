using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;

public class QuickSlot : TestBase
{
    private PlayerInput playerInput;
    Equip equip;
    private Player owner;
    public Player Owner => owner;
    public Equip Equip => equip;
    [Tooltip("생성할 아이템 프리펩 부모 오브젝트")]
    public GameObject itemParent;

    public delegate void OnQuickSlot();

    public event OnQuickSlot mainWeapon_01;
    public event OnQuickSlot mainWeapon_02;
    public event OnQuickSlot subWeapon;
    public event OnQuickSlot grenade;
    public event OnQuickSlot ect;

    public Action<ItemData> onMainWeapon01Change;
    public Action<ItemData> onMainWeapon02Change;
    public Action<ItemData> onSubWeaponChange;
    public Action<ItemData> onGranadeChange;
    public Action<ItemData> onETCChange;

    private void Awake()
    {
        equip = GetComponent<Equip>();
    }

    public QuickSlot(Equip playerEquip, Player owner)
    {
        equip = playerEquip;
        this.owner = owner;
        itemParent = Owner.transform.Find("FirePosition").gameObject;
    }

    private void OnEnable()
    {
        mainWeapon_01 += MainWeapon_01;
        mainWeapon_02 += MainWeapon_02;
        subWeapon += SubWeapon;
        grenade += Grenade;
        ect += EctCompare;
    }

    public void MainWeapon_01()
    {
        if (equip.slots[0] != null)
        {
            equip.slots[0].ItemData.itemPrefab.GetComponent<WeaponBase>().Use();
            onMainWeapon01Change?.Invoke(equip.slots[0].ItemData);
        }
    }

    public void MainWeapon_02()
    {
        if (equip.slots[1] != null)
        {
            equip.slots[1].ItemData.itemPrefab.GetComponent<WeaponBase>().Use();
            onMainWeapon02Change?.Invoke(equip.slots[1].ItemData);
        }
    }

    public void SubWeapon()
    {
        if (equip.slots[3] != null)
        {
            equip.slots[3].ItemData.itemPrefab.GetComponent<WeaponBase>().Use();
            onSubWeaponChange?.Invoke(equip.slots[3].ItemData);
        }
    }

    public void Grenade()
    {
        if (equip.slots[5] != null)
        {
            equip.slots[5].ItemData.itemPrefab.GetComponent<GrenadeBase>().Use();
            onGranadeChange?.Invoke(equip.slots[5].ItemData);
        }
    }

    public void EctCompare()
    {
        if (equip.slots[7] != null)
        {
            if (equip.slots[7].ItemData.itemType == ItemType.Key)
            {
                equip.slots[7].ItemData.itemPrefab.GetComponent<DoorKey>().Use();
            }
            else if (equip.slots[7].ItemData.itemType == ItemType.Trap)
            {
                equip.slots[7].ItemData.itemPrefab.GetComponent<TrapBase>().Use();

            }
            onETCChange?.Invoke(equip.slots[7].ItemData);
        }
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        // 주무기01
        mainWeapon_01.Invoke();
        Debug.Log("주무기01");
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        // 주무기02
        mainWeapon_02.Invoke();
        Debug.Log("주무기02");
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        // 권총
        subWeapon.Invoke();
        Debug.Log("권총");
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        // 수류탄
        grenade.Invoke();
        Debug.Log("수류탄");
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        // 기타
        ect.Invoke();
        Debug.Log("기타");
    }
}
