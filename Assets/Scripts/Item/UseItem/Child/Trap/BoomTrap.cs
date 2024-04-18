using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomTrap : TrapBase
{
    TrapModel model;
    List<EnemyBace> Enemys;
    Player players;

    private void Awake()
    {
        model = GetComponentInChildren<TrapModel>();
        model.OnActive += Use;
    }

    private void OnTriggerEnter(Collider other)
    {


        EnemyBace enemy = other.GetComponent<EnemyBace>();
        Player player = other.GetComponent<Player>();
        if (enemy != null)
        {
            Enemys.Add(enemy);
        }

        if (player != null)
        {
            players = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyBace enemy = other.GetComponent<EnemyBace>();
        Player player = other.GetComponent<Player>();
        if (enemy != null)
        {
            Enemys.Remove(enemy);
        }

        if (player != null)
        {
            players = null;
        }
    }

    public override void Use()
    {
        foreach (EnemyBace enemy in Enemys)
        {
            enemy.Die();
        }

        if (players != null)
        {
            players.Die();
        }
    }
}
