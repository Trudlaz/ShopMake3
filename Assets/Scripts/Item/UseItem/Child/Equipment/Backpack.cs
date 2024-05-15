using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Backpack : ItemBase
{
    private void Awake()
    {
        // 게임 오브젝트 이름을 가져옴
        string objectName = gameObject.name;

        // 문자열을 ItemCode 열거형으로 변환
        if (System.Enum.TryParse(objectName, out ItemCode itemCode))
        {
            SetItemCode(itemCode);
            Debug.Log("아이템 코드 : " + itemCode);
        }
        else
        {
            Debug.LogWarning("아이템 코드에서 이름을 찾지 못 했습니다: " + objectName);
        }
    }
    public override void Use()
    {
          
    }

    public override void UnUse()
    {

    }
    public override void Interact(ItemCode itemCode)
    {
        base.Interact(itemCode);
    }
}
