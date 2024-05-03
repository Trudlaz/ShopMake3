using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // 버튼 클릭시 호출될 메서드
    public void LoadScene(string InGameScene)
    {
        SceneManager.LoadScene(InGameScene);
    }
}
