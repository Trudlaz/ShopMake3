using UnityEngine;
using UnityEngine.InputSystem;

public class CamRotate : MonoBehaviour
{
    public float mouseSensitivity = 5f; // 마우스 감도
    public Transform playerBody; // 플레이어 몸체의 Transform 컴포넌트
    public LayerMask interactableLayerMask; // 상호작용 가능한 레이어 마스크

    private Vector2 lookInput; // 마우스 입력 값을 저장할 변수
    private float xRotation = 0f; // 카메라의 상하 회전을 제어할 변수

    private InputAction lookAction; // 마우스로 보는 방향을 변경하는 액션
    private InputAction interactAction; // 상호작용 액션
    private PlayerMove inputActions; // 사용자 입력을 처리하는 스크립트

    private void Awake()
    {
        inputActions = new PlayerMove(); // 입력 액션 초기화
        lookAction = inputActions.Player.Look;
        interactAction = inputActions.Player.InteractAction;
    }

    private void OnEnable()
    {
        lookAction.Enable();
        interactAction.Enable();
        lookAction.performed += OnLook;
        interactAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        lookAction.performed -= OnLook;
        interactAction.performed -= OnInteract;
        lookAction.Disable();
        interactAction.Disable();
    }


    private const float InteractDistance = 3f; // 상호작용 최대 거리

    private void OnInteract(InputAction.CallbackContext context)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractDistance, interactableLayerMask))
        {
            Debug.Log("Interacted with " + hit.collider.name);
            if (hit.collider.CompareTag("Door"))
            {
                hit.collider.transform.parent.SendMessage("Interect", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                hit.collider.SendMessage("Interaction", SendMessageOptions.DontRequireReceiver);
            }
        }
    }




    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>(); // 마우스 입력값 읽기

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 상하 회전 제한

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // 카메라 상하 회전 적용
        playerBody.Rotate(Vector3.up * mouseX); // 플레이어 몸체 좌우 회전 적용
    }
}
