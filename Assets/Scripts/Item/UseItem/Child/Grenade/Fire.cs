using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fire : MonoBehaviour
{
    public float totalTime = 10.0f;
    public float fireDamege = 5.0f;
    public float delay = 0.2f;

    float timer = 0;

    List<EnemyBace> targets = new List<EnemyBace>();

    private void Start()
    {
        StartCoroutine(targetDamege());
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyBace enemy = other.GetComponent<EnemyBace>();
        if (enemy != null)
        {
            targets.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyBace enemy = other.GetComponent<EnemyBace>();
        if (enemy != null)
        {
            targets.Remove(enemy);
        }
    }

    IEnumerator targetDamege()
    {
        while (timer < totalTime) 
        {
            yield return new WaitForSeconds(delay);

            foreach (EnemyBace enemy in targets)
            {
                enemy.Demage(fireDamege);
            }
        }
        Destroy(this.gameObject, 0.1f);
    }
}
