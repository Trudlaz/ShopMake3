using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item", order = 0)]
public class ItemData : ScriptableObject
{
    public ItemCode itemId;         // 아이템 ID
    public ItemType itemType;       // 아이템의 타입
    public string itemName;         // 아이템 이름
    public Sprite itemImage;        // 인벤토리에서 보일 이미지
    public uint weight;             // 아이템 무게
    public uint maxItemCount;       // 최대로 가질 수 있는 아이템의 개수
    public GameObject itemPrefab;   // 아이템
    public uint Price;              // 가격
    
}