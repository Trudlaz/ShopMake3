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
    private Player player; // Player 인스턴스 참조 추가

    private PlayerMove inputActions;
    private WeaponBase currentWeapon;

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

        // Player 인스턴스 참조
        player = GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player 컴포넌트를 찾을 수 없습니다.");
        }

        // 입력 시스템 설정
        inputActions = new PlayerMove();
        inputActions.Player.LeftMouse.performed += OnLeftMouse;
        inputActions.Player.RightMouse.performed += OnRightMouse;
        inputActions.Player.Reload.performed += OnReload;
        inputActions.Enable();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
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
        if (firePosition != null)
        {
            // 수류탄을 들고 있는지 확인하고 Use 메서드를 호출
            bool itemUsed = false;

            // BuffBase를 상속받은 모든 버프 아이템을 검색하고 Use 메서드를 호출
            BuffBase[] buffs = firePosition.GetComponentsInChildren<BuffBase>();
            foreach (BuffBase buff in buffs)
            {
                if (buff != null)
                {
                    buff.Initialize(player); // Player 인스턴스 설정
                    buff.Use();
                    Debug.Log($"버프 사용중: {buff}");
                    itemUsed = true;
                    break;
                }
            }

            if (!itemUsed)
            {
                // GrenadeBase를 상속받은 모든 수류탄 아이템을 검색하고 Use 메서드를 호출
                GrenadeBase[] grenades = firePosition.GetComponentsInChildren<GrenadeBase>();
                foreach (GrenadeBase grenade in grenades)
                {
                    if (grenade != null)
                    {
                        grenade.Use();
                        Debug.Log($"수류탄 사용중: {grenade}");
                        itemUsed = true;
                        break;
                    }
                }
            }

            if (!itemUsed)
            {
                Debug.Log("사용할 버프 또는 수류탄이 없습니다.");
            }
        }
        else
        {
            Debug.LogError("FirePosition 오브젝트가 설정되지 않았습니다.");
        }
    }
    private void OnReload(InputAction.CallbackContext context)
    {
        if (firePosition != null)
        {
            WeaponBase weapon = firePosition.GetComponentInChildren<WeaponBase>();

            if (weapon != null)
            {
                weapon.Use();
                Debug.Log("무기 장전 완료");
            }
            else
            {
                Debug.Log("장전 실패");
            }
        }
    }
}
