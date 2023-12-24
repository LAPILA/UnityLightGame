using UnityEngine;
using TMPro;

public class MoveTMPUp : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // TMP 오브젝트를 여기에 연결해주세요.
    public float stopY = 3900f; // 멈출 Y 위치
    public float moveSpeed = 50f; // 이동 속도

    private bool move = true; // 이동 여부 판단

    void Update()
    {
        if (move) {
            MoveText();
        }
    }

    void MoveText()
    {
        Vector3 pos = textMeshPro.rectTransform.localPosition;
        pos.y += moveSpeed * Time.deltaTime;
        textMeshPro.rectTransform.localPosition = pos;

        if (pos.y >= stopY) {
            move = false;
            // 게임 종료 로직 추가
        }
    }

    // 버튼 클릭 시 호출할 함수
    public void QuitGame()
    {
        Application.Quit();
    }
}
