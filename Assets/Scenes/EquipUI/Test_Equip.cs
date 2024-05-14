using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Equip : MonoBehaviour
{
    [Range(0, 35)]
    public uint invenFromIndex = 0;

    [Range(0, 35)]
    public uint invenToIndex = 0;

    [Range(0, 143)]
    public uint worldFfromIndex = 0;

    [Range(0, 143)]
    public uint worldToIndex = 0;

    public Equip equip;

    private void Start()
    {
        equip = GameManager.Instance.EquipUI.Equip;
        equip.AddItem(ItemCode.Shotgun);
        equip.AddItem(ItemCode.Pistol);
        equip.AddItem(ItemCode.Shotgun);
        equip.AddItem(ItemCode.Rifle);
        equip.AddItem(ItemCode.Key);
        equip.AddItem(ItemCode.Sniper);
    }
}
