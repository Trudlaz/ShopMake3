using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 플레이어 이동속도
    /// </summary>
    public float moveSpeed = 3f;

    /// 플레이어 점프력
    /// </summary>
    public float jumpPower = 5f;

    // 인벤토리 및 플레이어 UI 하면서 건든거 ----------------------------------------------------------------------/

    Inventory inven;

    public Inventory Inventory => inven;

    /// <summary>
    /// 플레이어의 최대 체력
    /// </summary>
    float maxHp = 100;

    public float MaxHp => maxHp;

    /// <summary>
    /// 플레이어 체력
    /// </summary>
    float hp = 100;
    public float Hp
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                hp = Mathf.Clamp(hp, 0, maxHp);
                onHealthChange?.Invoke(hp);
                if (hp < 0.1)
                {
                    Die();
                }
            }
        }
    }


    /// <summary>
    /// 플레이어의 무게
    /// </summary>
    float weight = 0;

    public float Weight
    {
        get => weight;
        set
        {
            if(weight != value)
            {
                weight = value;
                onWeightChange?.Invoke(weight);
                // 현재 무게에 따라 이속속도 가변하는 것 추가하기
            }
        }
    }

    // ---------------------------------------------------------------------------------------------------------/

    /// <summary>
    /// 달릴시 빨라지는 속도 
    /// </summary>
    public float runningSpeed = 2.0f;

    /// <summary>
    /// 캐릭터 컴포넌트 참조를 위한 변수 선언
    /// </summary>
    private CharacterController cc;

    /// <summary>
    /// 인풋 시스템 참조용 변수
    /// </summary>
    private PlayerMove inputActions;

    /// <summary>
    /// 움직임 좌표계산을 위한? 변수
    /// </summary>
    private Vector2 movementInput;

    /// <summary>
    /// 아래로 떨어지는 중력
    /// </summary>
    private float gravity = -20f;

    /// <summary>
    /// 캐릭터의 수직 속도 고정
    /// </summary>
    private float yVelocity = 0;

    /// <summary>
    /// 달리기 상태
    /// </summary>
    private bool isRunning = false;
    private Vector3 moveDirection = Vector3.zero;
    public float limitWeight = 20f; // 무게가 이 값보다 클 때 속도가 줄어드는 지점
    public float MaxWeight = 40f; // 무게가 이 값보다 클 때 이동이 멈추는 지점
    public float currentWeight = 10f; // 현재 무게

    /// <summary>
    /// 플레이어의 사망을 알리는 델리게이트
    /// </summary>
    public Action onDie;
    public Action<float> onHealthChange { get; set; }

    public Action<float> onWeightChange { get; set; }

    public float defense = 0.0f;

    public Animator animator;

    bool isMove = false;

    PlayerNoiseSystem noise;
    WaitForSeconds LangingSoundInterval = new WaitForSeconds(0.1f);
    const float runSoundRange = 5.0f;
    const float landingSoundRange = 6.0f;

    /// <summary>
    /// 플레이어가 공중에 있었는지 확인하는 변수
    /// </summary>
    private bool wasInAir = false;


    private void Awake()
    {
        inputActions = new PlayerMove();
        noise = transform.GetComponentInChildren<PlayerNoiseSystem>(true);
        noise.gameObject.SetActive(false);
    }


    // 점프시 레이를 이용해 점프할 수 있는 환경인지 확인
    private void OnEnable()
    {
        noise.gameObject.SetActive(false);
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerformed;  // 이동 시작
        inputActions.Player.Move.canceled += OnMoveCanceled;    // 이동 중지
        inputActions.Player.Jump.performed += OnJump;           // 점프
        inputActions.Player.Run.performed += OnRunPerformed;    // 달리기 시작
        inputActions.Player.Run.canceled += OnRunCanceled;      // 달리기 중지
        //inputActions.Player.InteractAction.performed += OnInteract;            // 상호작용
        inputActions.Player.InventoryAction.performed += OnInventoryAction;    // 인벤토리 사용
        inputActions.Player.InventoryAction.canceled += OnInventoryAction;     // 인벤토리 사용
        inputActions.Player.Exit.performed += OnExit;                          // 종료(일시정지)
        inputActions.Player.LeftMouse.performed += OnLeftMouse;                // 왼쪽 마우스 입력
        inputActions.Player.RightMouse.performed += OnRightMouse;              // 오른쪽 마우스 입력
        inputActions.Player.HotbarKey.performed += OnHotbarKey;                // 핫바키 사용
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Run.performed -= OnRunPerformed;
        inputActions.Player.Run.canceled -= OnRunCanceled;
        //inputActions.Player.InteractAction.performed -= OnInteract;
        inputActions.Player.InventoryAction.performed -= OnInventoryAction;
        inputActions.Player.InventoryAction.canceled -= OnInventoryAction;
        inputActions.Player.Exit.performed -= OnExit;
        inputActions.Player.LeftMouse.performed -= OnLeftMouse;
        inputActions.Player.RightMouse.performed -= OnRightMouse;
        inputActions.Player.HotbarKey.performed -= OnHotbarKey;
        inputActions.Player.Disable();
    }

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        inven = new Inventory(this);
        if(GameManager.Instance.InventoryUI != null )
        {
            GameManager.Instance.InventoryUI.InitializeInventory(Inventory);
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        isMove = true;
        if (isRunning)
        {
            noise.Radius = runSoundRange;
            noise.gameObject.SetActive(true);
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        movementInput = Vector2.zero;
        isMove = false;
        noise.gameObject.SetActive(false);
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (IsGrounded())
        {
            yVelocity = jumpPower;
        }
    }
    private bool IsGrounded()
    {
        float distanceToGround = cc.bounds.extents.y;
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.5f);

        // 레이 시각화를 위한 코드
        if (isGrounded)
        {
            Debug.DrawLine(transform.position, transform.position + (-Vector3.up * (distanceToGround + 0.5f)), Color.green);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + (-Vector3.up * (distanceToGround + 0.5f)), Color.red);
        }

        return isGrounded;
    }

    private void OnRunPerformed(InputAction.CallbackContext ctx)
    {
        isRunning = true;   // 달리는 중
        if (isMove) 
        {
            noise.Radius = runSoundRange;
            noise.gameObject.SetActive(true);
        }
    }

    private void OnRunCanceled(InputAction.CallbackContext ctx)
    {
        isRunning = false;  // 달리기 종료
        noise.gameObject.SetActive(false);
    }

    private void OnInventoryAction(InputAction.CallbackContext ctx)
    {
    }

    private void OnExit(InputAction.CallbackContext ctx)
    {
    }

    private void OnLeftMouse(InputAction.CallbackContext ctx)
    {
    }

    private void OnRightMouse(InputAction.CallbackContext ctx)
    {
    }

    private void OnHotbarKey(InputAction.CallbackContext ctx)
    {
        // int hotbarIndex = /* 여기에서 핫바 키 번호를 가져오는 코드 작성 */;
        // HotberKey(hotbarIndex);
    }




    private void FixedUpdate()
    {
        
        // Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
        if (Camera.main != null)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0;
            moveDirection = movementInput.x * cameraRight + movementInput.y * cameraForward;
        }
        float currentSpeed = moveSpeed;

        // 짐의 무게에 따라 이동 속도 조절
        if (currentWeight > limitWeight && currentWeight <= MaxWeight)
        {
            currentSpeed -= (currentWeight - limitWeight) / (MaxWeight - limitWeight) * (moveSpeed - 0f);
        }
        else if (currentWeight > MaxWeight)
        {
            currentSpeed = 0f;
        }

        currentSpeed = isRunning ? currentSpeed + runningSpeed : currentSpeed;
        moveDirection *= currentSpeed;
        if (IsGrounded())
        {
            yVelocity = Mathf.Max(0, yVelocity);
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        moveDirection.y = yVelocity;
        cc.Move(moveDirection * Time.deltaTime);

        bool isGroundedNow = IsGrounded();

        if (wasInAir && isGroundedNow)
        {
            Debug.Log("노이즈 발생!");
            // 공중에서 땅에 착지했을 때
            StartCoroutine(LandingNoise());
        }
        wasInAir = !isGroundedNow; // 현재 공중에 있는지 여부를 업데이트
    }

    public void Damege(float damege) 
    {
        hp -= (damege/defense);
    }

    public void Die()
    {
        // 조종이 불가하도록 만든다.
        inputActions.Player.Disable();


        // 델리게이트에 죽었음을 알리기(onDie 델리게이트)
        onDie?.Invoke();
    }

    IEnumerator LandingNoise()
    {
        noise.Radius = landingSoundRange;
        noise.gameObject.SetActive(true);
        yield return LangingSoundInterval;
        noise.gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.up, 5.0f);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.up, 5.0f * 0.6f); 
    }
#endif
}
