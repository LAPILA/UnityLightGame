using System.Collections;
using System.Collections.Generic;
using TMPro; // TMP(TextMeshPro) 네임스페이스 추가
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerAction : MonoBehaviour
{
    public float Speed;
    Rigidbody2D rigid;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;
    GameObject scanObject;

    int totalTorches = 8; // 총 횃불 개수
    int litTorches = 0;   // 켜진 횃불 개수
    public TextMeshProUGUI torchCountText; // TMP Text 객체

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        UpdateTorchCountText();
    }

    private void Update()
    {
        // 이동 입력 처리
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // 버튼 입력 상태 확인
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        // 수평 이동 여부 확인
        if (hDown || vUp)
            isHorizonMove = true;
        else if (vDown || hUp)
            isHorizonMove = false;

        // 이동 방향 설정
        if (vDown && v == 1) {
            dirVec = Vector3.up;
        }
        else if (vDown && v == -1) {
            dirVec = Vector3.down;
        }
        else if (hDown && h == -1) {
            dirVec = Vector3.left;
        }
        else if (hDown && h == 1) {
            dirVec = Vector3.right;
        }

        // 횃불 찾기
        if (Input.GetButtonDown("Jump") && scanObject != null && scanObject.layer == LayerMask.NameToLayer("object")) {
            // 횃불을 찾았을 때 동작
            litTorches++; // 켜진 횃불 개수 증가
            Destroy(scanObject); // 닿은 횃불 오브젝트 삭제
            scanObject = null; // scanObject 초기화

            // 남은 횃불 개수 갱신
            UpdateTorchCountText();

            // 모든 횃불을 찾았으면 게임 클리어 처리
            if (litTorches >= totalTorches) {
                // 게임 클리어 씬으로 전환 (SceneManager.LoadScene 사용)
                GameClear();
            }
        }
    }

    private void FixedUpdate()
    {
        // 이동 처리
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        // Ray 검사
        Debug.DrawRay(rigid.position, dirVec * 0.2f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.2f, LayerMask.GetMask("object"));

        if (rayHit.collider != null) {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 횃불이면
        if (collision.gameObject.layer == LayerMask.NameToLayer("object")) {
            // 횃불을 켜고 해당 오브젝트를 비활성화
            collision.gameObject.SetActive(false);
            litTorches++;

            // 남은 횃불 개수 갱신
            UpdateTorchCountText();

            // 모든 횃불을 켰으면 게임 클리어 처리
            if (litTorches >= totalTorches) {
                // 게임 클리어 씬으로 전환 (SceneManager.LoadScene 사용)
                GameClear();
            }
        }
    }

    // 텍스트 업데이트 함수
    void UpdateTorchCountText()
    {
        torchCountText.text = "Tourch: " + (totalTorches - litTorches).ToString();
    }

    void GameClear()
    {
        // 게임 클리어 시 수행할 동작 추가
        Debug.Log("게임 클리어!");
        // "GameClearScene"으로 전환
        SceneManager.LoadScene("GameClear");

    }
}
