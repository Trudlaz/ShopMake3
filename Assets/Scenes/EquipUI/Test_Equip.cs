using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Equip : MonoBehaviour
{
    public ItemCode code = ItemCode.SmallHeal;

    [Range(0, 35)]
    public uint invenFromIndex = 0;

    [Range(0, 35)]
    public uint invenToIndex = 0;

    [Range(0, 143)]
    public uint worldFfromIndex = 0;

    [Range(0, 143)]
    public uint worldToIndex = 0;

    //Inventory inven;

    public Inventory_UI inventoryUI;

    public Equip equip;

    public Equip_UI equip_UI;

    Player player;

    private void Start()
    {
        //player = GameManager.Instance.Player;

        //equip = new Equip(player, GameObject.Find("EquipSlots"));

        equip_UI = GameObject.Find("EquipBase").GetComponent<Equip_UI>();
        equip = equip_UI.Equip;
        equip.AddItem(ItemCode.Shotgun);
        //equip.AssignSlotItem(ItemCode.)
        Debug.Log(equip.slots[0]);
        //equip.AddItem(ItemCode.Pistol);
        //equip.AddItem(ItemCode.Shotgun);
        //equip.AddItem(ItemCode.Rifle);
        //equip.AddItem(ItemCode.Key);
        //equip.AddItem(ItemCode.Sniper);
    }
}
