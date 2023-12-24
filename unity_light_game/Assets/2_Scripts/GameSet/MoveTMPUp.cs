using UnityEngine;
using TMPro;

public class MoveTMPUp : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // TMP ������Ʈ�� ���⿡ �������ּ���.
    public float stopY = 3900f; // ���� Y ��ġ
    public float moveSpeed = 50f; // �̵� �ӵ�

    private bool move = true; // �̵� ���� �Ǵ�

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
            // ���� ���� ���� �߰�
        }
    }

    // ��ư Ŭ�� �� ȣ���� �Լ�
    public void QuitGame()
    {
        Application.Quit();
    }
}
