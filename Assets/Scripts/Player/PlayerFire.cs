using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition;
    public GameObject bombFactory;
    public float throwPower = 15f;
    public GameObject bulletEffect;
    private ParticleSystem ps;

    public int weaponPower = 5;

    private PlayerMove InputActions;


    PlayerNoiseSystem noise;

    private void Awake()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
        noise = transform.GetComponentInChildren<PlayerNoiseSystem>(true);

        InputActions = new PlayerMove();
        InputActions.Player.LeftMouse.performed += OnLeftMouse;
        InputActions.Player.RightMouse.performed += OnRightMouse;

        InputActions.Enable();
    }

    private void OnDestroy()
    {
        InputActions.Disable();
    }

    private void OnLeftMouse(InputAction.CallbackContext context)
    {
        Fire();
    }

    private void OnRightMouse(InputAction.CallbackContext context)
    {
        ThrowBomb();
    }

    private void Fire()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //EnemyBase eFSM = hitInfo.transform.gameObject.GetComponent<EnemyBase>();
                //if (eFSM != null)
                //{
                //    eFSM.Damage(weaponPower);
                //}
            }
            else
            {
                bulletEffect.transform.position = hitInfo.point;
                ps.Play();
            }
        }
    }

    private void ThrowBomb()
    {
        GameObject bomb = Instantiate(bombFactory, firePosition.transform.position, Quaternion.identity);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
    }
}
