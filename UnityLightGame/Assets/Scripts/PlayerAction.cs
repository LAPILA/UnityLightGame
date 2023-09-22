using System.Collections;
using System.Collections.Generic;
using TMPro; // TMP(TextMeshPro) ���ӽ����̽� �߰�
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

    int totalTorches = 8; // �� ȶ�� ����
    int litTorches = 0;   // ���� ȶ�� ����
    public TextMeshProUGUI torchCountText; // TMP Text ��ü

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        UpdateTorchCountText();
    }

    private void Update()
    {
        // �̵� �Է� ó��
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // ��ư �Է� ���� Ȯ��
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        // ���� �̵� ���� Ȯ��
        if (hDown || vUp)
            isHorizonMove = true;
        else if (vDown || hUp)
            isHorizonMove = false;

        // �̵� ���� ����
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

        // ȶ�� ã��
        if (Input.GetButtonDown("Jump") && scanObject != null && scanObject.layer == LayerMask.NameToLayer("object")) {
            // ȶ���� ã���� �� ����
            litTorches++; // ���� ȶ�� ���� ����
            Destroy(scanObject); // ���� ȶ�� ������Ʈ ����
            scanObject = null; // scanObject �ʱ�ȭ

            // ���� ȶ�� ���� ����
            UpdateTorchCountText();

            // ��� ȶ���� ã������ ���� Ŭ���� ó��
            if (litTorches >= totalTorches) {
                // ���� Ŭ���� ������ ��ȯ (SceneManager.LoadScene ���)
                GameClear();
            }
        }
    }

    private void FixedUpdate()
    {
        // �̵� ó��
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        // Ray �˻�
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
        // �浹�� ������Ʈ�� ȶ���̸�
        if (collision.gameObject.layer == LayerMask.NameToLayer("object")) {
            // ȶ���� �Ѱ� �ش� ������Ʈ�� ��Ȱ��ȭ
            collision.gameObject.SetActive(false);
            litTorches++;

            // ���� ȶ�� ���� ����
            UpdateTorchCountText();

            // ��� ȶ���� ������ ���� Ŭ���� ó��
            if (litTorches >= totalTorches) {
                // ���� Ŭ���� ������ ��ȯ (SceneManager.LoadScene ���)
                GameClear();
            }
        }
    }

    // �ؽ�Ʈ ������Ʈ �Լ�
    void UpdateTorchCountText()
    {
        torchCountText.text = "Tourch: " + (totalTorches - litTorches).ToString();
    }

    void GameClear()
    {
        // ���� Ŭ���� �� ������ ���� �߰�
        Debug.Log("���� Ŭ����!");
        // "GameClearScene"���� ��ȯ
        SceneManager.LoadScene("GameClear");

    }
}
