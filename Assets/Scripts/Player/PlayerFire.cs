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
    private WeaponBase weapon; // WeaponBase 타입의 멤버 변수 추가
    private QuickSlot quickSlot; // QuickSlot 클래스의 인스턴스 추가

    private void Awake()
    {
        // 파티클 시스템 컴포넌트 참조
        ps = bulletEffect.GetComponent<ParticleSystem>();
        if (ps == null)
        {
            Debug.LogError("BulletEffect에는 ParticleSystem 구성 요소가 포함되어 있지 않습니다.");
        }

        // 플레이어 노이즈 시스템 참조
        noise = transform.GetComponentInChildren<PlayerNoiseSystem>(true);
        if (noise == null)
        {
            Debug.LogError("플레이어 또는 해당 자식에서 플레이어 노이즈 시스템을 찾을 수 없습니다.");
        }

        // 메인 카메라 캐싱
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("메인 카메라를 찾을 수 없습니다. 카메라가 '메인 카메라'로 태그되어 있는지 확인하십시오.");
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
        // QuickSlot을 통해 아이템 사용 신호 보내기
        if (quickSlot != null)
        {
            //quickSlot.SubWeapon(); // 예시로 SubWeapon을 사용하도록 설정
            Debug.Log("퀵슬롯에서 아이템을 사용했습니다.");
        }
    }

    private void Fire()
    {
        if (weapon != null)
        {
            weapon.Fire(); // WeaponBase 클래스의 Fire 메서드 호출
        }
    }
}
