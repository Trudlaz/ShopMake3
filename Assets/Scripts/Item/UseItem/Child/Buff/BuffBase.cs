using System.Collections;
using UnityEngine;

public class BuffBase : ItemBase
{
    public float amountBuff = 10.0f;
    public float Maxduration = 10.0f;
    public uint itemIndex = 0; // 사용할 아이템 인덱스를 저장
    protected new Player player; // Player 참조

    public override void Use()
    {
        GameManager.Instance.EquipUI.UseItem(5);
    }

    public void Initialize(Player player)
    {
        this.player = player;
    }
}
