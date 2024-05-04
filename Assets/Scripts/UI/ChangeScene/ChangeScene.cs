using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    // 버튼 클릭 시 호출될 메서드
    public void LoadScene(string inGameScene)
    {
        GameManager.Instance.LoadSceneAsync(inGameScene);
    }
}
