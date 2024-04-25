using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerNoiseSystem : RecycleObject
{
    public float radius = 1.0f;     //소음 반경
    public Action<Transform> onPlayerFind;
    
    Transform origin;       //호출한 오브젝트의 위치
    public float Radius 
    {
        get => radius;
        set 
        {
            radius = value;
            sphere.radius = value;
        }
    }

    List<EnemyBace> enemies = new List<EnemyBace>();
    SphereCollider sphere;

    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
        origin = transform.parent.GetComponent<Transform>();
        sphere.radius = radius;
    }

    protected override void OnDisable()
    {
        if (enemies != null)
        {
            while (enemies.Count > 0)
            {
                enemies.Remove(enemies[0]);
            }
        }
        onPlayerFind = null;
        base.OnDisable();
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyBace enemy = other.GetComponent<EnemyBace>();
        if (enemy != null)
        {
            enemies.Add(enemy);
            
            foreach (EnemyBace entity in enemies)
            {
                onPlayerFind += (origin) => entity.OnDetect(origin, Radius);
            }
            onPlayerFind?.Invoke(origin);
        }
    }
}
