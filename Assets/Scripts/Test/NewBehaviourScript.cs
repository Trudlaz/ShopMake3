using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    [SerializeField]
    private bool _isActive;

    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (_isActive != value)
            {
                _isActive = value;
                OnActiveStateChanged();
            }
        }
    }

    private void Start()
    {
        IsActive = false; // 초기 값 설정, Inspector에서도 이 값을 조정할 수 있음
    }

    private void OnActiveStateChanged()
    {
        // IsActive 값이 변경될 때 실행할 로직
        Debug.Log($"IsActive is now {IsActive}");
    }
}
