using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunSlot : MonoBehaviour
{
    private Slot gunSlot;
    private Inventory inven;
    WeaponBase gun;

    private void Awake()
    {
        gunSlot = GetComponent<Slot>();
    }

    /// <summary>
    /// 총 리로딩 이벤트 등록 혹은 제거
    /// </summary>
    private void GunReloadEvent()
    {
        //WeaponBase gun;

        if (gunSlot.itemData != null)
        {
            gun = gunSlot.itemData.itemPrefab.GetComponent<WeaponBase>();
            //gun.onReload += inven.Reload(gun.ammunitionType, gun.maxAmmo);
        }
        else
        {
            //gun.onReload -= inven.Reload;
        }
    }
}
