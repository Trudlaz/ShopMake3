using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(bombEffect);
        eff.transform.position = transform.position;    // bombEffect와 오브젝트의 좌표를 동일하게 만든다. 
        Destroy(gameObject);    // 다른 콜라이더에 닿으면 폭탄 삭제
    }
}
