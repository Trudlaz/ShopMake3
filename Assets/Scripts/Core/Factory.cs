using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PoolObjectType
{
    Zombie = 0,
    Noise,
}

public class Factory : Singleton<Factory>
{
    ZombiePool zombiePool;
    NoisePool noisePool;
    public GameObject defaultItemPrefab;
    public float noisePower = 0.5f;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        zombiePool = GetComponentInChildren<ZombiePool>();
        if (zombiePool != null) zombiePool.Initialize();

        noisePool = GetComponentInChildren<NoisePool>();
        if (noisePool != null) noisePool.Initialize();
    }
    public GameObject GetObject(PoolObjectType type, Vector3? position = null, Vector3? euler = null)
    {
        GameObject result = null;
        switch (type)
        {
            case PoolObjectType.Zombie:
                result = zombiePool.GetObject(position, euler).gameObject;
                break;
        }

        return result;
    }

    public EnemyBace GetZombie()
    {
        return zombiePool.GetObject();
    }

    public EnemyBace GetZombie(Vector3 position, float angle = 0.0f)
    {
        return zombiePool.GetObject(position, angle * Vector3.forward);
    }

    public NoiseSystem GetNoise(float radius, Transform target)
    {
        NoiseSystem noise = noisePool.GetObject();
        noise.DataSet(radius, target);

        return noise;
    }

    public NoiseSystem GetNoise(float radius, Transform target, Vector3 position, float angle = 0.0f)
    {
        NoiseSystem noise = noisePool.GetObject(position, angle * Vector3.forward);
        noise.DataSet(radius, target);

        return noise;
    }

    //아이템을 생성하는 함수
    public GameObject MakeItem(GameObject prefab, Vector3 position, Transform transform, bool useNoise = false)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity, transform); // 프리팹을 인스턴스화하여 생성
        if (useNoise)
        {
            Vector2 rand = Random.insideUnitCircle * noisePower;
            obj.transform.position += new Vector3(rand.x, 0f, rand.y); // 노이즈 적용
        }
        return obj;
    }

    public GameObject MakeItem(GameObject prefab, Vector3 position, bool useNoise = false)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity); // 프리팹을 인스턴스화하여 생성
        if (useNoise)
        {
            Vector2 rand = Random.insideUnitCircle * noisePower;
            obj.transform.position += new Vector3(rand.x, 0f, rand.y); // 노이즈 적용
        }
        return obj;
    }

    // 아이템을 여러 개 생성하는 함수
    public GameObject[] MakeItems(GameObject prefab, uint count, Vector3 position, Transform transform, bool useNoise = false)
    {
        GameObject[] items = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            items[i] = MakeItem(prefab, position, transform, useNoise);
        }
        return items;
    }

    public GameObject[] MakeItems(GameObject prefab, uint count, Vector3 position, bool useNoise = false)
    {
        GameObject[] items = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            items[i] = MakeItem(prefab, position, useNoise);
        }
        return items;
    }

    // 아이템을 여러개 생성하는 함수 (프리팹을 교체할 때 기본 아이템 프리팹을 사용)
    public GameObject[] MakeItems(ItemCode code, uint count, Vector3 position, Transform transform, bool useNoise = false)
    {
        // 코드를 이용하여 아이템 프리팹을 가져옴 (이 부분은 코드에 따라서 다르게 구현될 수 있음)
        GameObject prefab = GetItemPrefabByCode(code);

        // 가져온 프리팹으로 아이템 생성
        if (prefab != null)
        {
            return MakeItems(prefab, count, position, transform, useNoise);
        }
        else
        {
            // 기본 아이템 프리팹을 사용하여 생성
            return MakeItems(defaultItemPrefab, count, position, transform, useNoise);
        }
    }

    public GameObject[] MakeItems(ItemCode code, uint count, Vector3 position, bool useNoise = false)
    {
        // 코드를 이용하여 아이템 프리팹을 가져옴 (이 부분은 코드에 따라서 다르게 구현될 수 있음)
        GameObject prefab = GetItemPrefabByCode(code);

        // 가져온 프리팹으로 아이템 생성
        if (prefab != null)
        {
            return MakeItems(prefab, count, position, useNoise);
        }
        else
        {
            // 기본 아이템 프리팹을 사용하여 생성
            return MakeItems(defaultItemPrefab, count, position, useNoise);
        }
    }

    // 아이템을 생성하는 함수 (프리팹을 교체할 때 기본 아이템 프리팹을 사용)
    public GameObject MakeItem(ItemCode code, Vector3 position, bool useNoise = false)
    {
        // 코드를 이용하여 아이템 프리팹을 가져옴 (이 부분은 코드에 따라서 다르게 구현될 수 있음)
        GameObject prefab = GetItemPrefabByCode(code);

        // 가져온 프리팹으로 아이템 생성
        if (prefab != null)
        {
            return MakeItem(prefab, position, transform, useNoise);
        }
        else
        {
            // 기본 아이템 프리팹을 사용하여 생성
            return MakeItem(defaultItemPrefab, position, transform, useNoise);
        }
    }

    // 아이템 프리팹을 코드에 따라 가져오는 함수
    private GameObject GetItemPrefabByCode(ItemCode code)
    {
        return GameManager.Instance.ItemData[code].itemPrefab;
    }
}