using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Purchasing;
using UnityEngine;

public class QuickSlot : MonoBehaviour
{
    //private Player player;
    //private GameObject quickSlot;
    //private Action quickSlotSelect;
    //public Equip equip;
    //GameObject item;
    //[Tooltip("생성할 아이템 오브젝트의 부모 오브젝트")]
    //[SerializeField] private GameObject itemPostion;

    //private void Awake()
    //{
    //    equip = FindObjectOfType<Equip>(true);
    //    quickSlot = Fin
    //}

    //private void Start()
    //{
    //    player = FindObjectOfType<Player>();
    //}

    /// <summary>
    /// 선택한 아이템 오브젝트 생성
    /// </summary>
    /// <param name="index"></param>
    //public void ItemObjectCreate(uint index)
    //{
    //    if (item == null)
    //    {
    //        item = equip.slots[index].ItemData.itemPrefab;
    //        Instantiate(item, itemPostion.transform.position, Quaternion.identity, transform);
    //    }
    //    else
    //    {
    //        Destroy(item);
    //        item = equip.slots[index].ItemData.itemPrefab;
    //    }
    //}

    //public void ItemInputEventEnroll()
    //{
    //     아이템의 use 함수 이벤트 등록
    //}


    private PlayerInput playerInput;
    public Equip equip;

    //public Event mainWeapon_01;
    //public Action mainWeapon_02;
    //public Action subWeapon;
    //public Action grenade;
    // public Action ect;

    public delegate void OnQuickSlot();

    public event OnQuickSlot mainWeapon_01;
    public event OnQuickSlot mainWeapon_02;
    public event OnQuickSlot subWeapon;
    public event OnQuickSlot grenade;
    public event OnQuickSlot ect;

    private void Start()
    {
        equip = FindObjectOfType<Equip>();
    }

    private void OnEnable()
    {
        mainWeapon_01 += MainWeapon_01;
    }

    public void MainWeapon_01()
    {
        equip.slots[0].ItemData.itemPrefab.GetComponent<WeaponBase>().Use();
    }

    public void MainWeapon_02()
    {
        equip.slots[1].ItemData.itemPrefab.GetComponent<WeaponBase>().Use();
    }

    public void SubWeapon()
    {
        equip.slots[3].ItemData.itemPrefab.GetComponent<WeaponBase>().Use();
    }

    public void Grenade()
    {
        equip.slots[0].ItemData.itemPrefab.GetComponent<GrenadeBase>().Use();
    }

    public void EctCompare ()
    {
        var ectComponents = equip.slots[7].ItemData.itemPrefab.GetComponents<Component>();
        DoorKey door = new DoorKey();
        TrapBase trap = new TrapBase();

        foreach (var component in ectComponents)
        {
            if (component.GetType() == door.GetType())
            {
                equip.slots[7].ItemData.itemPrefab.GetComponent<DoorKey>().Use();

                break;
            }
            else if (component.GetType() == trap.GetType())
            {
                equip.slots[7].ItemData.itemPrefab.GetComponent<TrapBase>().Use();

                break;
            }
        }
    }
}
