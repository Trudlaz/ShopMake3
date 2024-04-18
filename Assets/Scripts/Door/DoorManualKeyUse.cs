using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManualKeyUse : DoorManual
{
    public override void Interect()
    {
        Open();
    }

    protected override bool preOpen()
    {
        bool result = true;
        //플레이어 인벤토리 검사 (키있을시 true)
        return result;
    }

    protected override void Open()
    {
        if (preOpen()) 
        {
            Debug.Log("탈출");
            //화면전환(게임메뉴)
        }
    }
}
