using System.Collections;
using UnityEngine;

public class Monster_Hide : MonoBehaviour
{
    public Game game; // 게임 관리 객체 참조
    private float timeInArea = 0; // 범위 내에 있는 시간을 추적
    private bool playerInArea = false; // 플레이어가 범위 안에 있는지 여부

    void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    void Update()
    {
        // 플레이어가 범위 안에 있고, 0.5초 이상 있었을 경우 게임 오버
        if (playerInArea) {
            timeInArea += Time.deltaTime;
            if (timeInArea >= 0.5f) {
                GameOver();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            playerInArea = true; // 플레이어가 범위 안에 들어옴
            timeInArea = 0; // 시간 추적 시작
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            playerInArea = false; // 플레이어가 범위 밖으로 나감
            timeInArea = 0; // 시간 추적 리셋
        }
    }

    private void GameOver()
    {
        Debug.Log("게임 오버!");
        if (game != null) {
            game.GameOver();
        }
    }
}
