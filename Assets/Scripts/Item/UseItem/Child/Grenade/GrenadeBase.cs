using System.Collections;
using UnityEngine;

public class GrenadeBase : ItemBase
{
    [Tooltip("소음반경")]
    public float NoiseRange = 5.0f;
    public GameObject expoltionEffect;

    protected bool isActive = false;

    Rigidbody rb;
    PlayerFire playerfire;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // 초기에는 중력을 사용하지 않음
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
        // Use 호출 시 필요한 참조를 미리 가져오기
        playerfire = GetComponentInParent<PlayerFire>();
        Player player = GameManager.Instance.Player;
        Transform cam = player.transform.GetChild(0);

        // 수류탄을 부모 오브젝트에서 분리
        transform.parent = null;

        // 수류탄 위치 및 물리 설정
        transform.position = playerfire.firePosition.transform.position;
        isActive = true;
        rb.useGravity = true; // Use 메서드가 호출될 때 중력을 사용
        rb.AddForce(cam.forward * playerfire.throwPower, ForceMode.Impulse);

        Debug.Log(isActive);

        GameManager.Instance.EquipUI.UseItem(3);
    }

}
