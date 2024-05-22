using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlotNumber : MonoBehaviour
{
    QuickType[] quickTypes;

    Player player;

    private void Awake()
    {
        quickTypes = GetComponentsInChildren<QuickType>();
        player = GetComponentInParent<Player>();
    }

    public bool SwapItem(Equipment equip, Transform obj, bool isEquip)
    {
        bool result = true;

        Transform child;
        if (isEquip)
        {
            switch (player.Equipment)
            {
                case Equipment.Gun:
                    obj.GetChild(0).parent = quickTypes[0].transform;
                    quickTypes[0].transform.GetChild(0).position = transform.position;
                    break;
                case Equipment.Throw:
                    obj.GetChild(0).parent = quickTypes[3].transform;
                    quickTypes[3].transform.GetChild(0).position = transform.position;
                    break;
                case Equipment.ETC:
                    obj.GetChild(0).parent = quickTypes[5].transform;
                    quickTypes[5].transform.GetChild(0).position = transform.position;
                    break;
                default:
                    result = false;
                    break;
            }

            switch (equip)
            {
                case Equipment.Gun:
                    quickTypes[0].transform.GetChild(0).parent = obj;
                    player.Equipment = quickTypes[0].Equipment;
                    obj.GetChild(0).position = obj.position;
                    break;
                case Equipment.Throw:
                    quickTypes[3].transform.GetChild(0).parent = obj;
                    player.Equipment = quickTypes[3].Equipment;
                    obj.GetChild(0).position = obj.position;
                    break;
                case Equipment.ETC:
                    quickTypes[5].transform.GetChild(0).parent = obj;
                    player.Equipment = quickTypes[5].Equipment;
                    obj.GetChild(0).position = obj.position;
                    break;
                default:
                    result = false;
                    break;
            }

            result = true;
        }
        else
        {
            switch (equip)
            {
                case Equipment.Gun:
                    quickTypes[0].transform.GetChild(0).parent = obj;
                    player.Equipment = quickTypes[0].Equipment;
                    obj.GetChild(0).position = obj.position;
                    break;
                case Equipment.Throw:
                    quickTypes[3].transform.GetChild(0).parent = obj;
                    player.Equipment = quickTypes[3].Equipment;
                    obj.GetChild(0).position = obj.position;
                    break;
                case Equipment.ETC:
                    quickTypes[5].transform.GetChild(0).parent = obj;
                    player.Equipment = quickTypes[5].Equipment;
                    obj.GetChild(0).position = obj.position;
                    break;
                default:
                    result = false;
                    break;
            }
        }
        return result;
    }

    public bool AddItem(GameObject obj, Equipment type)
    {
        bool result = false;

        for(int i = 0; i < quickTypes.Length; i++)
        {
            if (type == quickTypes[i].Equipment && quickTypes[i].IsEmpty)
            {
                Instantiate(obj, quickTypes[i].transform);
                result = true;
                break;
            }
        }

        return result;
    }
}
