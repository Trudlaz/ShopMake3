using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    
    public float interval = 1.5f;
    public int totalSpawn = 4;

    NavMeshAgent navMesh;


    private void Start()
    {
        MakeZombie();
    }

    //private void Update()
    //{
    //    navMesh.SetDestination(transform.position);

    //}




    private void MakeZombie()
    {
        StartCoroutine(SpawnCoroutine());
    }


    IEnumerator SpawnCoroutine()
    {
        for(int i = 0; i < totalSpawn; i++)
        {
            yield return new WaitForSeconds(interval);
            Spawn();
        }
        
    }

    void Spawn()
    {
        EnemyBace zombie = Factory.Instance.GetZombie(GetSpawnPosition());
        zombie.transform.SetParent(transform);
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 pos = transform.position;
        //pos.z += Random.Range();

        return pos;
    }

}
