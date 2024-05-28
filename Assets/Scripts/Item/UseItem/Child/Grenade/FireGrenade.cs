using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGrenade : GrenadeBase
{
    public GameObject fire;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (isActive)
        {
            if (collision.transform.CompareTag("Ground"))
            {
                Explode();
                Destroy(this.gameObject, 1f);
            }
        }
    }

    protected override void Explode()
    {
        base.Explode();
        Instantiate(fire, transform.position, Quaternion.identity);
        Instantiate(expoltionEffect, transform.position, Quaternion.identity);
    }
}
