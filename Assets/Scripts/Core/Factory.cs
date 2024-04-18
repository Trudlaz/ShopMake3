using System.Collections;
using System.Collections.Generic;
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

    public NoiseSystem GetNoise(float radius, Transform target,  Vector3 position, float angle = 0.0f)
    {
        NoiseSystem noise = noisePool.GetObject(position, angle * Vector3.forward);
        noise.DataSet(radius, target);

        return noise;
    }
}