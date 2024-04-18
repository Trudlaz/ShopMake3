using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : TrapBase
{
    private void OnTriggerEnter(Collider other)
    {
        other.CompareTag("Enemy");
        {
            EnemyBace enemy = other.GetComponent<EnemyBace>();
            enemy.moveSpeed -= amount;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.CompareTag("Enemy");
        {
            EnemyBace enemy = other.GetComponent<EnemyBace>();
            enemy.moveSpeed += amount;
        }
    }
}
