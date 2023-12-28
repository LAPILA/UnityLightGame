using System.Collections;
using UnityEngine;

public class Monster_Hide : MonoBehaviour
{
    public Game game; // ���� ���� ��ü ����
    private float timeInArea = 0; // ���� ���� �ִ� �ð��� ����
    private bool playerInArea = false; // �÷��̾ ���� �ȿ� �ִ��� ����

    void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    void Update()
    {
        // �÷��̾ ���� �ȿ� �ְ�, 1�� �̻� �־��� ��� ���� ����
        if (playerInArea) {
            timeInArea += Time.deltaTime;
            if (timeInArea >= 1.0f) {
                GameOver();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            playerInArea = true; // �÷��̾ ���� �ȿ� ����
            timeInArea = 0; // �ð� ���� ����
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            playerInArea = false; // �÷��̾ ���� ������ ����
            timeInArea = 0; // �ð� ���� ����
        }
    }

    private void GameOver()
    {
        Debug.Log("���� ����!");
        if (game != null) {
            game.GameOver();
        }
    }
}
