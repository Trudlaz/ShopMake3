using UnityEngine;
using UnityEngine.UI; // UI 버튼과 상호작용하기 위해 필요함

public class QuitButton : MonoBehaviour
{
    public Button quitButton; // 에디터에서 할당할 버튼

    void Start()
    {
        quitButton.onClick.AddListener(QuitApplication); // 버튼에 이벤트 리스너 추가
    }

    void QuitApplication()
    {
        // 애플리케이션 종료
        Debug.Log("애플리케이션 종료 요청됨");
        Application.Quit();
    }
}
