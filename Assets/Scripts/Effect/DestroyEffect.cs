using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float destroyTime = 1.5f;

    private void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }
}
