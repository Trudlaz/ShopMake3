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
    private WeaponBase currentWeapon;
    private BuffBase currentBuff;
    private ArmorBase currentArmor;

    PlayerNoiseSystem noise;
    private QuickSlot quickSlot;

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
        // 매번 좌클릭 시 currentWeapon을 갱신
        if (firePosition != null)
        {
            WeaponBase[] weapons = firePosition.GetComponentsInChildren<WeaponBase>();
            if (weapons.Length > 0)
            {
                currentWeapon = weapons[0];
                currentWeapon.InitializeEffects(bulletEffect, ps); // WeaponBase 이펙트 초기화
            }
            else
            {
                currentWeapon = null;
            }
        }

        if (currentWeapon != null)
        {
            currentWeapon.Fire();
        }
        else
        {
            Debug.Log("맨손 상태로 공격할 수 없습니다.");
        }
    }

    private void OnRightMouse(InputAction.CallbackContext context)
    {
        // BuffBase와 ArmorBase를 처리
        if (firePosition != null)
        {
            BuffBase[] buffs = firePosition.GetComponentsInChildren<BuffBase>();
            ArmorBase[] armors = firePosition.GetComponentsInChildren<ArmorBase>();

            if (buffs.Length > 0)
            {
                currentBuff = buffs[0];
                currentBuff.Use(); // BuffBase 사용
                Debug.Log($"버프 사용중: {currentBuff}");
            }
            else if (armors.Length > 0)
            {
                currentArmor = armors[0];
                currentArmor.Use(); // ArmorBase 사용
                Debug.Log($"방어구 사용중: {currentArmor}");
            }
            else
            {
                currentBuff = null;
                currentArmor = null;
                Debug.Log("버프 또는 방어구가 없습니다.");
            }
        }
        else
        {
            Debug.LogError("FirePosition 오브젝트가 설정되지 않았습니다.");
        }
    }
}
