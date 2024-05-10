using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBase : ItemBase
{
    [Tooltip("소음반경")]
    public float NoiseRange = 5.0f;
    public GameObject expoltionEffect;

    protected bool isActive = false;

    Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (isActive)
        {
            Explode();
        }
    }

    protected virtual void Explode()
    {
        Factory.Instance.GetNoise(NoiseRange, transform);
    }

    public override void Use()
    {
        PlayerFire playerfire = GetComponentInParent<PlayerFire>();         //물건을 사용할때는 무조건 자식으로 들어가 있을것
        Player player = GameManager.Instance.Player;
        Transform cam = player.transform.GetChild(0);

        transform.position = playerfire.firePosition.transform.position;
        isActive = true;
        rb.AddForce(cam.forward * playerfire.throwPower, ForceMode.Impulse);
    }
}