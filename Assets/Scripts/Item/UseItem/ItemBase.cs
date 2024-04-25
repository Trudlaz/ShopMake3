using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemBase : MonoBehaviour
{
    public int Price = 1000;
    public float Weight = 3.0f;

    public virtual void Use()
    {

    }

    //public ItemData Interaction(ItemCode itemCode)
    //{

    // gpt 이용한 코드라 변경 가능성이 있습니다. 뭐가 맞는 코드인지 잘 몰라서 여러개 작성했습니다.

        //ItemData itemData = new ItemData();
        //itemData.Price = Price;
        //itemData.Weight = Weight;
        //// 아이템 데이터에 관련 정보를 설정할 수 있습니다.

        //// 실제로는 아이템 프리팹을 생성하여 인벤토리에 넣을 수 있습니다.
        //// 여기서는 단순히 아이템 데이터만 생성하여 반환합니다.

        //return itemData;

        //(ItemCode itemCode) 없음

        //-----------------------------------------

        //Inventory.instance.AddItem(item);   // 인벤토리에 아이템 추가하기
        //Destroy(gameObject);

        //------------------------------------------------------------

        //// 플레이어가 아이템을 주웠다고 가정하고, 아이템 데이터를 생성합니다.
        //ItemData itemData = new ItemData(Price, Weight);

        //// 생성된 아이템 데이터를 인벤토리에 추가합니다.
        //Inventory.Instance.AddItem(itemData);

        //// 아이템 오브젝트를 비활성화하여 화면에서 제거합니다.
        //gameObject.SetActive(false);

        // -----------------------------------------------------------

        //// 플레이어가 아이템을 주웠다고 가정하고, 아이템 데이터를 생성합니다.
        //ItemData itemData = new ItemData(Price, Weight);

        //// 생성된 아이템 데이터를 인벤토리에 추가합니다.
        //Inventory.Instance.AddItem(itemData);

        //// 아이템 오브젝트를 비활성화하여 화면에서 제거합니다.
        //gameObject.SetActive(false);

        //// 아이템 데이터를 반환합니다.
        //return itemData;

        // ------------------------------------------------

        //ItemData itemData = GameManager.Instance.ItemData[itemCode]; // 아이템 데이터를 아이템 코드를 이용하여 가져옴

        //// 인벤토리에 아이템을 추가하는데 성공하면 true를 반환하고, 실패하면 false를 반환
        //bool success = AddItem(itemCode);

        //if (success)
        //{
        //    return itemData; // 아이템을 성공적으로 추가했을 경우 아이템 데이터 반환
        //}
        //else
        //{
        //    return null; // 아이템 추가에 실패했을 경우 null 반환
        //}
        //}
    }





