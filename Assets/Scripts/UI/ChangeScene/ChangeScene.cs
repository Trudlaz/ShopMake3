using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    // 버튼 클릭 시 호출될 메서드
    public void ChangeToStartGameScene(string inGameScene)
    {
        GameManager.Instance.StartGame(inGameScene);
    }
}
