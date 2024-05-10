using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct ItemSpawnInfo
    {
        public ItemCode code;
        [Range(0, 1)]
        public float dropRatio;
        public uint maxDropCount;
    }
    public ItemSpawnInfo[] dropItems;

    Transform[] spawnPoints;

    const int ItemCount = 33;

    private void Awake()
    {
        spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < spawnPoints.Length; i++) 
        {
            spawnPoints[i] = transform.GetChild(i);
        }
    }

    private void Start()    //여기서 델리게이트 연결
    {
        GameManager.Instance.OnGameEnding += Clear;
        GameManager.Instance.OnGameStartCompleted += Spawn;
    }

    void Spawn() //델리게이트 받아서 실행
    {
        foreach (Transform point in spawnPoints)
        {
            int dataIndex;
            do
            {
                dataIndex = Random.Range(0, ItemCount);
            }
            while (dropItems[dataIndex].dropRatio < Random.value);

            uint count = (uint)Random.Range(1, dropItems[dataIndex].maxDropCount);
            Factory.Instance.MakeItems(dropItems[dataIndex].code, count, point.position, point, true);
        }
    }

    void Clear() //델리게이트 받아서 실행//자기 자식 싹 정리
    {
        foreach (Transform point in spawnPoints) 
        {
            while (point.childCount > 0)     // 자식이 남아있으면 계속 반복
            {
                Transform child = point.GetChild(0);    // 첫번째 자식 선택
                child.SetParent(null);                      // 부모 제거(Destroy가 즉시 실행되지 않기 때문에 필요)
                Destroy(child.gameObject);                  // 자식 삭제
            }
        }
    }

#if UNITY_EDITOR
    public void Test_Spawn() 
    {
        Spawn();
    }
    public void Test_Clear() 
    {
        Clear();
    }
#endif
}
