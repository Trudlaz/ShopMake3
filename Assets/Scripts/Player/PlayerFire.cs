using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition;
    public GameObject bombFactory;
    public float throwPower = 15f;
    public GameObject bulletEffect;
    private ParticleSystem ps;
    private Camera mainCamera;
    public int weaponPower = 5;

    private PlayerMove InputActions;


    PlayerNoiseSystem noise;

    private void Awake()
    {
        // 파티클 시스템 컴포넌트 참조
        ps = bulletEffect.GetComponent<ParticleSystem>();
        if (ps == null)
        {
            Debug.LogError("BulletEffect does not contain a ParticleSystem component.");
        }

        // 플레이어 노이즈 시스템 참조
        noise = transform.GetComponentInChildren<PlayerNoiseSystem>(true);
        if (noise == null)
        {
            Debug.LogError("No PlayerNoiseSystem found on the player or its children.");
        }

        // 메인 카메라 캐싱
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Make sure your camera is tagged as 'MainCamera'.");
        }

        // 입력 시스템 설정
        InputActions = new PlayerMove();
        InputActions.Player.LeftMouse.performed += OnLeftMouse;
        InputActions.Player.RightMouse.performed += OnRightMouse;
        InputActions.Enable();
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }


    private void OnLeftMouse(InputAction.CallbackContext context)
    {
        Fire();
    }

    private void OnRightMouse(InputAction.CallbackContext context)
    {
        // 여기서 액션을 보내면, 퀵슬롯에서 슬롯을 현재 장비하고 있는 아이템과 연결 해주는 것 
    }

    private void Fire()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        float maxDistance = 100f;  // 예를 들어 최대 100미터
        int layerMask = LayerMask.GetMask("Enemy");

        //if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
        //{
        //    // 적중한 객체 처리
        //    EnemyBase eFSM = hitInfo.transform.GetComponent<EnemyBase>();
        //    if (eFSM != null)
        //    {
        //        eFSM.Damage(weaponPower);
        //    }
        //}
        //else
        //{
        //    bulletEffect.transform.position = hitInfo.point;
        //    ps.Play();
        //}
    }



}
